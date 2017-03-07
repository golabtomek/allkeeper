using System.Windows;
using WpfClipboardMonitor;

namespace Allkeeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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

    }
}
