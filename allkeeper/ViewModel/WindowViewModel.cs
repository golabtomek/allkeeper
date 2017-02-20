using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace allkeeper.ViewModel
{
    public partial class MainViewModel : INotifyPropertyChanged
    {
        #region constructor
        public MainViewModel()
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            width = 1;
            height = 1;
            left = (int)desktopWorkingArea.Left;
            top = (int)desktopWorkingArea.Top;
            SetColors();
            SystemParameters.StaticPropertyChanged += SystemParameters_StaticPropertyChanged;
            SearchBarForeground = Brushes.LightGray;
            searchBarText = "Search";
            this.ClipboardUpdateCommand = new DelegateCommand(OnClipboardUpdate, OnCanClipboardUpdate);
        }
        #endregion

        #region windowProperties
        
        private int _height;
        public int height
        {
            get { return _height; }
            set
            {
                if (value == _height) return;
                _height = value;
                RaisePropertyChanged("height");
            }
        }

        private int _width;
        public int width
        {
            get { return _width; }
            set
            {
                if (value == _width) return;
                _width = value;
                RaisePropertyChanged("width");
            }
        }

        private int _top;
        public int top
        {
            get { return _top; }
            set
            {
                if (value == _top) return;
                _top = value;
                RaisePropertyChanged("top");
            }
        }

        private int _left;
        public int left
        {
            get { return _left; }
            set
            {
                if (value == _left) return;
                _left = value;
                RaisePropertyChanged("left");
            }
        }
        

        #endregion

        #region WindowColors

        private void SystemParameters_StaticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "WindowGlassBrush")
            {
                SetColors();
            }
        }


        private void SetColors()
        {
            background = SystemParameters.WindowGlassBrush;
        }

        private Brush _background;
        public Brush background
        {
            get { return _background; }
            set
            {
                if (value == _background) return;
                _background = value;
                RaisePropertyChanged("background");
            }
        }

        private Brush _textboxBorder;
        public Brush textboxBorder
        {
            get { return _textboxBorder; }
            set
            {
                if (value == _textboxBorder) return;
                _textboxBorder = value;
                RaisePropertyChanged("textboxBorder");
            }
        }
        #endregion

        #region Commands
#endregion

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
