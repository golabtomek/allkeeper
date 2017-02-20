using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;

namespace allkeeper.ViewModel
{
    public partial class MainViewModel : INotifyPropertyChanged
    {
#region constructor
        public MainViewModel()
        {
            width = 1;
            height = 1;
            minTopProperty = 1 - height;
            topProperty = minTopProperty;
            minLeftProperty = 1 - width;
            leftProperty = minLeftProperty;
            SetColors();
            SystemParameters.StaticPropertyChanged += SystemParameters_StaticPropertyChanged;
            this.ClipboardUpdateCommand = new DelegateCommand(OnClipboardUpdate, OnCanClipboardUpdate);
        }
        #endregion

        #region windowProperties

        public int minTopProperty { get; set; }

        private int _topProperty;
        public int topProperty
        {
            get { return _topProperty; }
            set
            {
                if (value == _topProperty) return;
                _topProperty = value;
                RaisePropertyChanged("topProperty");
            }
        }

        public int minLeftProperty { get; set; }

        private int _leftProperty;
        public int leftProperty
        {
            get { return _leftProperty; }
            set
            {
                if (value == _leftProperty) return;
                _leftProperty = value;
                RaisePropertyChanged("leftProperty");
            }
        }

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

        

        #endregion


        #region Colors

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

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
