﻿#region using

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using HBD.Framework.EventArguments;

#endregion

namespace HBD.Framework.Core
{
    public class ChangeTrackingEntry<TEntity> : IDisposable
        where TEntity : class, ICancelableNotifyPropertyChanging, INotifyPropertyChanged
    {
        public ChangeTrackingEntry(TEntity entity)
        {
            Guard.ArgumentIsNotNull(entity, nameof(entity));
            Entity = entity;

            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
            if (Entity is IInternalNotifyPropertyChanged && Entity is IInternalNotifyPropertyChanging)
            {
                ((IInternalNotifyPropertyChanging) Entity).InternalPropertyChanging += Entity_PropertyChanging;
                ((IInternalNotifyPropertyChanged) Entity).InternalPropertyChanged += Entity_PropertyChanged;
            }
            else
            {
                Entity.PropertyChanging += Entity_PropertyChanging;
                Entity.PropertyChanged += Entity_PropertyChanged;
            }
        }

        public TEntity Entity { get; private set; }

        public virtual ConcurrentDictionary<string, object> OriginalValues { get; } =
            new ConcurrentDictionary<string, object>();

        public bool IsChanged { get; private set; }

        public void Dispose()
        {
            if (Entity == null) return;

            var entity = Entity as IInternalNotifyPropertyChanged;
            if (entity != null && Entity is IInternalNotifyPropertyChanging)
            {
                ((IInternalNotifyPropertyChanging) Entity).InternalPropertyChanging -= Entity_PropertyChanging;
                entity.InternalPropertyChanged -= Entity_PropertyChanged;
            }
            else
            {
                Entity.PropertyChanging -= Entity_PropertyChanging;
                Entity.PropertyChanged -= Entity_PropertyChanged;
            }

            Entity = null;
        }

        /// <summary>
        ///     This will be remove the original of properties accept the current values as latest one.
        /// </summary>
        public void AcceptChanges()
        {
            if (!IsChanged) return;

            lock (OriginalValues)
            {
                var evnArg = new CancelEventArgs();

#if !NETSTANDARD1_6
                OnAcceptChange(evnArg);
#endif
                if (evnArg.Cancel) return;

                OriginalValues.Clear();
                IsChanged = false;
            }
        }

        public void UndoChanges()
        {
            if (!IsChanged) return;

            lock (OriginalValues)
            {
                var evnArg = new CancelEventArgs();

#if !NETSTANDARD1_6
                OnUndoChange(evnArg);
#endif

                if (evnArg.Cancel) return;

                Entity.PropertyChanging -= Entity_PropertyChanging;
                Entity.PropertyChanged -= Entity_PropertyChanged;

                foreach (var item in OriginalValues) Entity.SetPropertyValue(item.Key, item.Value);

                Entity.PropertyChanging += Entity_PropertyChanging;
                Entity.PropertyChanged += Entity_PropertyChanged;

                IsChanged = false;
            }
        }

        private void Entity_PropertyChanged(object sender, PropertyChangedEventArgs e)
            => IsChanged = true;

        private void Entity_PropertyChanging(object sender, CancelablePropertyChangingEventArgs e)
        {
            if (e.Cancel) return;
            OriginalValues.GetOrAdd(e.PropertyName, Entity.PropertyValue(e.PropertyName));
        }

#if !NETSTANDARD1_6
        public event EventHandler<CancelEventArgs> AcceptChange;

        public event EventHandler<CancelEventArgs> UndoChange;

        protected virtual void OnAcceptChange(CancelEventArgs e)
            => AcceptChange?.Invoke(this, e);

        protected virtual void OnUndoChange(CancelEventArgs e)
            => UndoChange?.Invoke(this, e);
#endif
    }
}