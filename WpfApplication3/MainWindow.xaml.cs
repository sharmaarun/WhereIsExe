/**
 * Author       : Arun Sharma
 * Version      : 1.0
 * Description  : Dummy Setup ~ Main Page
 **
 */



using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace SetupEx
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        StackPanel tmpPanel = null;
       

        public MainWindow()
        {
            log4net.Config.XmlConfigurator.Configure();
           
            InitializeComponent();

            
            
        }

        private void SwitchToInstallation(object sender, RoutedEventArgs e)
        {
            if (!AgreeField.IsChecked.Value)
            {
                System.Windows.MessageBox.Show("Please accept terms and conditions!", "Information"); ;
            }
            else
            {

                /*C_ResourceLoader rl = new C_ResourceLoader();
                rl.LoadFile(System.AppDomain.CurrentDomain.FriendlyName);
                rl.LoadResource(C_Constants.ID_RESOURCE_ZIP_DATA_NAME, C_Constants.ID_RESOURCE_ZIP_DATA_TYPE);

                C_ZipExtractor extractor = new C_ZipExtractor();
                extractor.ExtractArchive(rl.GetCurrentResourceData(), PathField.Text);*/
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PathField.Text = dlg.SelectedPath;
            }
        }


        private void switchPage(StackPanel fromPanel, StackPanel toPanel)
        {

            Storyboard story = new Storyboard();

            DoubleAnimation dbWidth = new DoubleAnimation();
            dbWidth.From = 1;
            dbWidth.To = 0;
            dbWidth.Duration = new Duration(TimeSpan.FromSeconds(0.25));



            story.Children.Add(dbWidth);

            Storyboard.SetTarget(dbWidth, fromPanel);
            Storyboard.SetTargetProperty(dbWidth, new PropertyPath(System.Windows.Controls.StackPanel.OpacityProperty));


            story.Completed += new EventHandler(switchNextPage);
            tmpPanel = toPanel;
            story.Begin();
        }

        private void switchNextPage(object obj, EventArgs args)
        {
            tmpPanel.Visibility = System.Windows.Visibility.Visible;
        }

       

        



        
    }
}
