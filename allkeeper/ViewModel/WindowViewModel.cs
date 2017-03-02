using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Forms;
using System.Collections.Generic;

namespace allkeeper.ViewModel
{
    enum WindowPosition { up, down }

    public partial class MainViewModel : INotifyPropertyChanged
    {
        #region constructor
        public MainViewModel()
        {
            WindowConstructor();
            ClipboardConstructor();
            NotesConstructor();
        }
        #endregion

        #region windowViewModel
        private void WindowConstructor()
        {
            loadWindowPosition();
            setWindowDimensions();
            setWindowPosition();
            SetColors();
            SystemParameters.StaticPropertyChanged += SystemParameters_StaticPropertyChanged;
        }
        
        #region WindowProperties
        private double fullWidth = 900;
        private double currentWidth;

        private WindowPosition windowPosition;
        
        public void saveWindowPosition()
        {
            if (windowPosition == WindowPosition.up) Model.XmlFile.saveWindowSettings("up");
            if (windowPosition == WindowPosition.down) Model.XmlFile.saveWindowSettings("down");
        }

        public void loadWindowPosition()
        {
            string position = Model.XmlFile.loadWindowSettings();
            if (position == "up") windowPosition = WindowPosition.up;
            else if (position == "down") windowPosition = WindowPosition.down;
            else windowPosition = WindowPosition.up;
        }

        private void setWindowDimensions()
        {
            width = 1;
            height = 1;
            currentWidth = fullWidth;
            
        }

        private void setWindowPosition()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            switch (windowPosition)
            {
                case WindowPosition.down:
                    {
                        left = (int)desktopWorkingArea.Left;
                        top = (int)desktopWorkingArea.Bottom - 1;
                        break;
                    }
                case WindowPosition.up:
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

        private double _height;
        public double height
        {
            get { return _height; }
            set
            {
                if (value == _height) return;
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
                if (value == _width) return;
                _width = value;
                RaisePropertyChanged("width");
            }
        }


        private int _gridHeight;
        public int gridHeight
        {
            get { return _gridHeight; }
            set
            {
                if (value == _gridHeight) return;
                _gridHeight = value;
                RaisePropertyChanged("gridHeight");
            }
        }

        private double _top;
        public double top
        {
            get { return _top; }
            set
            {
                if (value == _top) return;
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
                if (value == _left) return;
                _left = value;
                RaisePropertyChanged("left");
            }
        }
        
        #endregion

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
        private ICommand _ShowApp;
        public ICommand ShowApp
        {
            get
            {
                if(_ShowApp == null)
                {
                    _ShowApp = new RelayCommand(
                        o =>
                        {
                            var desktopWorkingArea = SystemParameters.WorkArea;
                            height = 450;
                            gridHeight = 450;
                            if (windowPosition == WindowPosition.down)
                            {
                                top = desktopWorkingArea.Bottom - height;
                            }
                            else top = desktopWorkingArea.Top;
                            width = currentWidth;
                        });
                }
                return _ShowApp;
            }
        }

        private ICommand _HideApp;
        public ICommand HideApp
        {
            get
            {
                if (_HideApp == null)
                {
                    _HideApp = new RelayCommand(
                        o =>
                        {
                            var desktopWorkingArea = SystemParameters.WorkArea;
                            currentWidth = width;
                            width = 1;
                            height = 1;
                            gridHeight = 1;
                            if (windowPosition == WindowPosition.down)
                            {
                                top = desktopWorkingArea.Bottom - 1;
                            }
                            else top = desktopWorkingArea.Top;
                        });
                }
                return _HideApp;
            }
        }

        private ICommand _WindowBottom;
        public ICommand WindowBottom
        {
            get
            {
                if(_WindowBottom == null)
                {
                    _WindowBottom = new RelayCommand(
                        o =>
                        {
                            var desktopWorkingArea = SystemParameters.WorkArea;
                            windowPosition = WindowPosition.down;
                            setWindowPosition();
                        });
                }
                return _WindowBottom;
            }
        }

        private ICommand _WindowTop;
        public ICommand WindowTop
        {
            get
            {
                if (_WindowTop == null)
                {
                    _WindowTop = new RelayCommand(
                        o =>
                        {
                            var desktopWorkingArea = SystemParameters.WorkArea;
                            windowPosition = WindowPosition.up;
                            setWindowPosition();
                        });
                }
                return _WindowTop;
            }
        }

        private ICommand _SaveWindowPosition;
        public ICommand SaveWindowPosition
        {
            get
            {
                if(_SaveWindowPosition == null)
                {
                    _SaveWindowPosition = new RelayCommand(
                        o =>
                        {
                            saveWindowPosition();
                        });
                }
                return _SaveWindowPosition;
            }
        }
        #endregion

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
