using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClipboardMonitor;

namespace allkeeper
{

    public partial class MainWindow : ClipboardMonitorWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void shutdownTray_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private void SetStartup()
        {
            string path = "C:/Users/@username@/AppData/Roaming/Microsoft/Windows/Start Menu/Programs/Startup";
            if(!File.Exists(path + "/allkeeper.lnk"))
            {

            }
        }

        public static void Create(string fullPathToLink, string fullPathToTargetExe, string startIn, string description)
        {

        }
    }
}
