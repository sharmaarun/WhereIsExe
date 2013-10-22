/**
 * Author       : Arun Sharma
 * Version      : 1.0
 * Description  : All constants used throughout the program
 **
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SetupEx
{
    public class C_Constants
    {

        /*
         * All constants - replacement of hardcoding
         */

        public enum ReturnCode
        {
            SUCCESS = 0,
            FAILED
        }

        public const string ID_RESOURCE_ZIP_DATA_NAME = "2000";
        public const string ID_RESOURCE_ZIP_DATA_TYPE  = "SETUP";



    }
}
