using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Allkeeper.ViewModel
{

    enum WindowPosition { top, bottom }

    public class Window : INotifyPropertyChanged
    {
        private WindowPosition windowPosition;
        private double _height;
        public double height
        {
            get { return _height; }
            set
            {
                if (_height == value) return;
                _height = value;
                RaisePropertyChanged("height");
            }
        }

        private double _width;
        public double width
        {
            get { return _width; }
            set
            {
                if (_width == value) return;
                _width = value;
                RaisePropertyChanged("width");
            }
        }

        private double _top;
        public double top
        {
            get { return _top; }
            set
            {
                if (_top == value) return;
                _top = value;
                RaisePropertyChanged("top");
            }
        }

        private double _left;
        public double left
        {
            get { return _left; }
            set
            {
                if (_left == value) return;
                _left = value;
                RaisePropertyChanged("left");
            }
        }

        private double fullHeight = 450;

        public Window()
        {
            loadWindowPosition();
            setDimensions(false);
            setPosition(false);
            SystemParameters.StaticPropertyChanged += SystemParameters_StaticPropertyChanged;
            SetColors();
        }
        
        public void saveWindowPosition()
        {
            if (windowPosition == WindowPosition.top) Model.XmlFiles.saveWindowSettings("top");
            if (windowPosition == WindowPosition.bottom) Model.XmlFiles.saveWindowSettings("bottom");
        }

        public void loadWindowPosition()
        {
            string position = Model.XmlFiles.loadWindowSettings();
            if (position == "top") windowPosition = WindowPosition.top;
            else if (position == "bottom") windowPosition = WindowPosition.bottom;
            else windowPosition = WindowPosition.top;
        }

        private void setPosition(bool isFullSize)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            if (isFullSize == false)
            {
                switch (windowPosition)
                {
                    case WindowPosition.bottom:
                        {
                            left = (int)desktopWorkingArea.Left;
                            top = (int)desktopWorkingArea.Bottom - 1;
                            break;
                        }
                    case WindowPosition.top:
                        {
                            left = (int)desktopWorkingArea.Left;
                            top = (int)desktopWorkingArea.Top;
                            break;
                        }
                    default:
                        {
                            left = (int)desktopWorkingArea.Left;
                            top = (int)desktopWorkingArea.Top;
                            break;
                        }
                }
            }
            else
            {
                switch (windowPosition)
                {
                    case WindowPosition.bottom:
                        {
                            left = (int)desktopWorkingArea.Left;
                            top = (int)desktopWorkingArea.Bottom - fullHeight;
                            break;
                        }
                    case WindowPosition.top:
                        {
                            left = (int)desktopWorkingArea.Left;
                            top = (int)desktopWorkingArea.Top;
                            break;
                        }
                    default:
                        {
                            left = (int)desktopWorkingArea.Left;
                            top = (int)desktopWorkingArea.Top;
                            break;
                        }
                }
            }
        }

        private void setDimensions(bool toFullSize)
        {
            if (toFullSize == true)
            {
                Rect desktopWorkingArea = SystemParameters.WorkArea;
                width = (desktopWorkingArea.Width / 3) * 2;
                height = fullHeight;
            }
            else
            {
                width = 1;
                height = 1;
            }
        }

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

        #region Commands

        private ICommand _setWindowPositionToBottom;
        public ICommand setWindowPositionToBottom
        {
            get
            {
                if (_setWindowPositionToBottom == null)
                {
                    _setWindowPositionToBottom = new RelayCommand(
                        o =>
                        {
                            windowPosition = WindowPosition.bottom;
                            setPosition(false);
                        });
                }
                return _setWindowPositionToBottom;
            }
        }

        private ICommand _setWindowPositionToTop;
        public ICommand setWindowPositionToTop
        {
            get
            {
                if (_setWindowPositionToTop == null)
                {
                    _setWindowPositionToTop = new RelayCommand(
                        o =>
                        {
                            windowPosition = WindowPosition.top;
                            setPosition(false);
                        });
                }
                return _setWindowPositionToTop;
            }
        }

        private ICommand _showApp;
        public ICommand showApp
        {
            get
            {
                if (_showApp == null)
                {
                    _showApp = new RelayCommand(
                        o =>
                        {
                            setDimensions(true);
                            setPosition(true);
                        });
                }
                return _showApp;
            }
        }

        private ICommand _hideApp;
        public ICommand hideApp
        {
            get
            {
                if(_hideApp == null)
                {
                    _hideApp = new RelayCommand(
                        o =>
                        {
                            setDimensions(false);
                            setPosition(false);
                        });
                }
                return _hideApp;
            }
        }

        private ICommand _saveWindowSettings;
        public ICommand saveWindowSettings
        {
            get
            {
                if(_saveWindowSettings == null)
                {
                    _saveWindowSettings = new RelayCommand(
                        o =>
                        {
                            saveWindowPosition();
                        });
                }
                return _saveWindowSettings;
            }
        }
#endregion

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
