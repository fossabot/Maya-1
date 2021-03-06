//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//
//
// <copyright file="Album.cs" company="Multinerd">
//     Copyright (c) Multinerd. All Rights Reserved.
// </copyright>
//------------------------------------------------------------------------------

using Prism.Mvvm;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EntityPlus
{
	[GeneratedCode("Tessa", "1.2.3.4")]
	/// <summary> Album model class.</summary>
	public partial class Album : BindableBase, IEditableObject
    {
        /// <summary> Initializes a new instance of the <see cref="Album"/> class.</summary>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Album()
        {
            this.Tracks = new HashSet<Track>();
        }

        /// <summary>Gets or sets the AlbumId property</summary>
        [Key]
        [DisplayName("Album Id"), Display(Name = "Album Id")]
        public int AlbumId
        {
            get { return this._AlbumId; }
            set { this.SetProperty(ref this._AlbumId, value); }
        }

        /// <summary> AlbumId backing field</summary>
        private int _AlbumId;


        /// <summary>Gets or sets the Title property</summary>
		[Required, StringLength(160)]
		[DisplayName("Title"), Display(Name = "Title")]
		public string Title
        {
            get { return this._Title; }
			set { this.SetProperty(ref this._Title, value); }
        }

		/// <summary> Title backing field</summary>
        private string _Title;


        /// <summary>Gets or sets the ArtistId property</summary>
		[Required]
		[DisplayName("Artist Id"), Display(Name = "Artist Id")]
		public int ArtistId
        {
            get { return this._ArtistId; }
			set { this.SetProperty(ref this._ArtistId, value); }
        }

		/// <summary> ArtistId backing field</summary>
        private int _ArtistId;


		/// <summary> Gets or sets the Artist navigation property</summary>
		public virtual Artist Artist { get; set; }

		/// <summary> Gets or sets the Tracks navigation property</summary>
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Track> Tracks { get; set; }

	        
		#region IEditableObject

		private Album_Backup _Album_Backup;

		private struct Album_Backup 
		{
			internal int _AlbumId;
			internal string _Title;
			internal int _ArtistId;
		}

        public void BeginEdit()
        {
			_Album_Backup = new Album_Backup() 
			{
				_AlbumId = AlbumId,
				_Title = Title,
				_ArtistId = ArtistId,
			};
		}

		public void CancelEdit()
        {
			AlbumId = _Album_Backup._AlbumId;
			Title = _Album_Backup._Title;
			ArtistId = _Album_Backup._ArtistId;
        }

        public void EndEdit()
        {
			_Album_Backup._AlbumId = AlbumId;
			_Album_Backup._Title = Title;
			_Album_Backup._ArtistId = ArtistId;
        }

		#endregion
 
		#region SampleData

		public static Album GetRandomAlbum()
        {
            return new Album()
            {
				AlbumId = 1,
				Title = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				ArtistId = 120261
            };
        }

		public static IList<Album> GetRandomAlbums()
        {
			var list = new List<Album>();
			
			list.Add(new Album()
			{
				AlbumId = 2,
				Title = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				ArtistId = 62969
			});

			list.Add(new Album()
			{
				AlbumId = 3,
				Title = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				ArtistId = 64622
			});

			list.Add(new Album()
			{
				AlbumId = 4,
				Title = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				ArtistId = 8493
			});

			list.Add(new Album()
			{
				AlbumId = 5,
				Title = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				ArtistId = 63816
			});

			list.Add(new Album()
			{
				AlbumId = 6,
				Title = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				ArtistId = 97921
			});

			return list;
        }

		#endregion
 

	} 
} 

