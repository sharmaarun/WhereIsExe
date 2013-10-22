using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace SetupEx
{
    /// <summary>
    /// Load/Add/Extract/Update resources from within or from external EXE/DLL
    /// </summary>
    class C_ResourceLoader
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(System.AppDomain.CurrentDomain.FriendlyName);


        /// <summary>
        /// Various return codes...
        /// </summary>
        

        private IntPtr ipHandleToModule;
        private IntPtr ipHandleToResource;
        public byte[] pResourceBytes;

        /// <summary>
        /// Find a particular resource wihtin the executable/library based on params.
        /// </summary>
        /// <param name="hModule"></param>
        /// <param name="lpName"></param>
        /// <param name="lpType"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern IntPtr FindResource(IntPtr hModule, string lpName, string lpType);

        /// <summary>
        /// Load a resource into the main memory
        /// </summary>
        /// <param name="hModule"></param>
        /// <param name="hResInfo"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

        /// <summary>
        /// calculate actual size of a resource
        /// </summary>
        /// <param name="hModule"></param>
        /// <param name="hResInfo"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern uint SizeofResource(IntPtr hModule, IntPtr hResInfo);

        /// <summary>
        /// Load EXE/LIBRARY into the main memory {For fetching a resource}
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr LoadLibrary(string path);



        /// <summary>
        /// Load Module {EXE/DLL} into memory
        /// </summary>
        /// <param name="filePath">Path to EXE/DLL module.</param>
        /// <returns></returns>
        public C_Constants.ReturnCode LoadFile(string filePath)
        {
            ipHandleToModule = LoadLibrary(filePath);
            if ((int)ipHandleToModule > 0)
                return C_Constants.ReturnCode.SUCCESS;
            else
            {
                log.Error("Error loading file!!");
                return C_Constants.ReturnCode.FAILED;
            }
        }

        
        /// <summary>
        /// Load resource into memory and get its byte[] data
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="resourceType"></param>
        /// <returns>ReturnCode</returns>
        public C_Constants.ReturnCode LoadResource(string resourceName, string resourceType)
        {
            try
            {
                ipHandleToResource = FindResource(ipHandleToModule, resourceName, resourceType);
                IntPtr ptrData = LoadResource(ipHandleToModule, ipHandleToResource);
                uint sizeOfResource = GetSizeOfCurrentLoadedResource();
                pResourceBytes = new byte[sizeOfResource];
                Marshal.Copy(ptrData, pResourceBytes,0, (int)sizeOfResource);
                return C_Constants.ReturnCode.SUCCESS;
            }
            catch (Exception ex)
            {
                log.Error("Error while loading resource", ex);
                return C_Constants.ReturnCode.FAILED;
            }
        }


        /// <summary>
        /// Get size of currently loaded resource
        /// </summary>
        /// <returns>Size of resource</returns>
        public uint GetSizeOfCurrentLoadedResource()
        {
            return SizeofResource(ipHandleToModule, ipHandleToResource);
        }

        public byte[] GetCurrentResourceData()
        {
            return pResourceBytes;
        }


    }
}
