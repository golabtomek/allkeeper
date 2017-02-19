using System;
using System.Collections.Generic;
using System.Linq;
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
            SearchBar.AddHandler(FrameworkElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(SearchBar_MouseLeftButtonDown), true);
            SearchBar.AddHandler(FrameworkElement.LostFocusEvent, new RoutedEventHandler(SearchBar_LostFocus), true);
        }
        

        private void SearchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text == "")
            {
                SearchBar.Text = "Search";
                SearchBar.Foreground = Brushes.LightGray;
            };

        }

        private void SearchBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (SearchBar.Text == "Search")
            {
                SearchBar.Text = "";
                SearchBar.Foreground = Brushes.White;
            }
        }

        private void shutdownTray_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
