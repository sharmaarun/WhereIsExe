using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace SetupEx
{
    class C_Switcher
    {
        public static C_Pages objSwitcher;

        public static void Switch(UserControl newPage)
        {
            objSwitcher.Navigate(null,newPage);
        }
    }
}
