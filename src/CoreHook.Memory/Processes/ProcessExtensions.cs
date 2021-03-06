﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace CoreHook.Memory.Processes
{
    public static class ProcessExtensions
    {
        public static bool Is64Bit(this Process process)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return Environment.Is64BitOperatingSystem;
            }

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new PlatformNotSupportedException("Platform not supported for detecting process architecture.");
            }

            if (!Environment.Is64BitOperatingSystem)
            {
                return false;
            }
         
            SafeProcessHandle processHandle = Interop.Kernel32.OpenProcess(
                Interop.Advapi32.ProcessOptions.PROCESS_QUERY_INFORMATION,
                false,
                process.Id);

            if (processHandle.IsInvalid)
            {
                throw new Win32Exception("Failed to open process query handle.");
            }

            using (processHandle)
            {
                bool processIsWow64 = false;
                if (!Interop.Kernel32.IsWow64Process(processHandle, ref processIsWow64))
                {
                    throw new Win32Exception("Determining process architecture with IsWow64Process failed.");
                }

                return !processIsWow64;
            }
        }
    }
}
