﻿using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Maya.CrashReporter.Helpers
{
    internal static class OSHelper
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll")]
        static extern IntPtr GetModuleHandle(string moduleName);

        [DllImport("kernel32")]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll")]
        static extern bool IsWow64Process(IntPtr hProcess, out bool wow64Process);

        private static bool Is64BitOS
        {
            get
            {
                // Check if this process is natively an x64 process. If it is, it will only run on x64 environments, thus, the environment must be x64.
                if (IntPtr.Size == 8)
                    return true;
                // Check if this process is an x86 process running on an x64 environment.
                IntPtr moduleHandle = GetModuleHandle("kernel32");
                if (moduleHandle != IntPtr.Zero)
                {
                    IntPtr processAddress = GetProcAddress(moduleHandle, "IsWow64Process");
                    if (processAddress != IntPtr.Zero)
                    {
                        if (IsWow64Process(GetCurrentProcess(), out var result) && result)
                            return true;
                    }
                }
                // The environment must be an x86 environment.
                return false;
            }
        }

        private static string HKLM_GetString(string key, string value)
        {
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(key);
                return registryKey?.GetValue(value).ToString() ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetOSVersion()
        {
            if (!string.IsNullOrEmpty(HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentMajorVersionNumber")))
            {
                return $"{HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentMajorVersionNumber")}.{HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentMinorVersionNumber")}.{HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuildNumber")}.0";
            }
            return $"{HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentVersion")}.{HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuildNumber")}.0";
        }

        public static string GetWindowsVersion()
        {
            string osArchitecture;
            try
            {
                osArchitecture = Is64BitOS ? "64-bit" : "32-bit";
            }
            catch (Exception)
            {
                osArchitecture = "32/64-bit (Undetermined)";
            }

            string productName = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "ProductName");
            string csdVersion = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CSDVersion");
            string currentBuild = HKLM_GetString(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentBuildNumber");
            if (!string.IsNullOrEmpty(productName))
            {
                return
                    $"{(productName.StartsWith("Microsoft") ? "" : "Microsoft ")}{productName}{(!string.IsNullOrEmpty(csdVersion) ? " " + csdVersion : string.Empty)} {osArchitecture} (OS Build {currentBuild})";
            }
            return string.Empty;
        }
    }
}
