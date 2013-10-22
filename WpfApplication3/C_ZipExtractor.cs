using Ionic.Zip;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SetupEx
{
    /// <summary>
    /// Zip extraction wrapper for DotNetZip library.
    /// <code>Performs operation on bye[] data</code>
    /// </summary>
    class C_ZipExtractor
    {

        ILog log = LogManager.GetLogger(System.AppDomain.CurrentDomain.FriendlyName);


        public C_Constants.ReturnCode ExtractArchive(byte[] archiveData, string path)
        {
            try
            {
                MemoryStream ms = new MemoryStream(archiveData);
                ZipFile zip = ZipFile.Read(ms);

                //Check if the path directory exists or not
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                }

                //extract each file in zip to the directory

                foreach (ZipEntry entry in zip)
                {
                    entry.Extract(path, ExtractExistingFileAction.OverwriteSilently);
                }
                return C_Constants.ReturnCode.SUCCESS;
            }
            catch (Exception ex)
            {
                log.Error("Error while Extracting", ex);
                return C_Constants.ReturnCode.FAILED;
            }

        }
        
    }
}
