using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace allkeeper.ViewModel
{
    public partial class MainViewModel
    {
        #region ClipboardVM
        public void ClipboardConstructor()
        {
            clipboardSearchBarForeground = Brushes.LightGray;
            clipboardSearchBarText = "Search";
            this.ClipboardUpdateCommand = new DelegateCommand(OnClipboardUpdate, OnCanClipboardUpdate);
            clipboardGridWidth = fullWidth / 2;
            clipboardCollapseButtonText = "Hide Clipboard History";
        }

        private Model.ClipboardModel clipboardModel = new Model.ClipboardModel();

        private ObservableCollection<string> _clipboard;
        public ObservableCollection<string> clipboard
        {
            get
            {
                return _clipboard;
            }
            set
            {
                if (_clipboard == value) return;
                _clipboard = value;
                RaisePropertyChanged("clipboard");
            }
        }

        public void ClipboardModelSync()
        {
            clipboard = new ObservableCollection<string>(clipboardModel.get());
        }
        

        private void copyToClipboard(string text)
        {
            clipboardModel.CopyToClipboard(text);
        }
        #region ClipboardProperties
        private double _clipboardGridWidth;
        public double clipboardGridWidth
        {
            get { return _clipboardGridWidth; }
            set
            {
                if (value == _clipboardGridWidth) return;
                _clipboardGridWidth = value;
                RaisePropertyChanged("clipboardGridWidth");
            }
        }

        private string _clipboardCollapseButtonText;
        public string clipboardCollapseButtonText
        {
            get { return _clipboardCollapseButtonText; }
            set
            {
                if (value == _clipboardCollapseButtonText) return;
                _clipboardCollapseButtonText = value;
                RaisePropertyChanged("clipboardCollapseButtonText");
            }
        }

        private Visibility _clipboardTitleVisibility;
        public Visibility clipboardTitleVisibility
        {
            get { return _clipboardTitleVisibility; }
            set
            {
                if (value == _clipboardTitleVisibility) return;
                _clipboardTitleVisibility = value;
                RaisePropertyChanged("clipboardTitleVisibility");
            }
        }

        private Visibility _clipboardClearButtonVisibility;
        public Visibility clipboardClearButtonVisibility
        {
            get { return _clipboardClearButtonVisibility; }
            set
            {
                if (value == _clipboardClearButtonVisibility) return;
                _clipboardClearButtonVisibility = value;
                RaisePropertyChanged("clipboardClearButtonVisibility");
            }
        }
        
        #endregion
        #region clipboardupdate
        public DelegateCommand ClipboardUpdateCommand { get; private set; }
        
        public void OnClipboardUpdate()
        {
            clipboardModel.addItem();
            ClipboardModelSync();
        }


        public bool OnCanClipboardUpdate()
        {
            return true;
        }

        #endregion
#region SearchBarProperties
        private string _clipboardSearchBarText;
        public string clipboardSearchBarText
        {
            get { return _clipboardSearchBarText; }
            set
            {
                if (_clipboardSearchBarText == value)
                    return;
                _clipboardSearchBarText = value;
                RaisePropertyChanged("clipboardSearchBarText");
                search();
            }
        }

        private Brush _clipboardSearchBarForeground;
        public Brush clipboardSearchBarForeground
        {
            get { return _clipboardSearchBarForeground; }
            set
            {
                if (_clipboardSearchBarForeground == value) return;
                _clipboardSearchBarForeground = value;
                RaisePropertyChanged("clipboardSearchBarForeground");
            }
        }

        public void search()
        {
                clipboardModel.search(clipboardSearchBarText);
                ClipboardModelSync();
        }

        #endregion

        #region Commands

        private ICommand _ClipboardSearchBarLeftButtonDown;
        public ICommand ClipboardSearchBarLeftButtonDown
        {
            get
            {
                if (_ClipboardSearchBarLeftButtonDown == null)
                    _ClipboardSearchBarLeftButtonDown = new RelayCommand(
                        o =>
                        {
                            if(clipboardSearchBarText=="Search")
                                clipboardSearchBarText = "";
                            clipboardSearchBarForeground = Brushes.White;
                        });
                return _ClipboardSearchBarLeftButtonDown;
            }
        }

        private ICommand _ClipboardSearchBarLostFocus;
        public ICommand ClipboardSearchBarLostFocus
        {
            get
            {
                if (_ClipboardSearchBarLostFocus == null)
                    _ClipboardSearchBarLostFocus = new RelayCommand(
                        o =>
                        {
                            if (clipboardSearchBarText == "")
                            {
                                clipboardSearchBarText = "Search";
                                clipboardSearchBarForeground = Brushes.LightGray;
                            }
                        });
                return _ClipboardSearchBarLostFocus;
                
            }
        }

        private ICommand _ClipboardClear;
        public ICommand ClipboardClear
        {
            get
            {
                if (_ClipboardClear == null)
                    _ClipboardClear = new RelayCommand(
                        o =>
                        {
                            clipboardModel.clear();
                            ClipboardModelSync();
                        });
                return _ClipboardClear;
            }
        }

        private ICommand _ClipboardDeleteItem;
        public ICommand ClipboardDeleteItem
        {
            get
            {
                if (_ClipboardDeleteItem == null)
                    _ClipboardDeleteItem = new RelayCommand(
                        o =>
                        {
                            string item = (string)o;
                            clipboardModel.removeItem(item);
                            ClipboardModelSync();
                        });
                return _ClipboardDeleteItem;
            }
        }

        private ICommand _ClipboardGridCollapse;
        public ICommand ClipboardGridCollapseExtend
        {
            get
            {
                if (_ClipboardGridCollapse == null)
                    _ClipboardGridCollapse = new RelayCommand(
                        o =>
                        {
                            if (clipboardClearButtonVisibility == Visibility.Visible)
                            {
                                width = width - clipboardGridWidth + 25;
                                clipboardGridWidth = 25;
                                clipboardCollapseButtonText = "Extend Clipboard History";
                                clipboardClearButtonVisibility = Visibility.Collapsed;
                                clipboardTitleVisibility = Visibility.Collapsed;
                            }
                            else
                            {
                                clipboardGridWidth = fullWidth / 2;
                                width = width + clipboardGridWidth - 25;
                                clipboardCollapseButtonText = "Hide Clipboard History";
                                clipboardClearButtonVisibility = Visibility.Visible;
                                clipboardTitleVisibility = Visibility.Visible;
                            }
                        });
                return _ClipboardGridCollapse;
            }
        }

        private ICommand _ClipboardCopyItem;
        public ICommand ClipboardCopyItem
        {
            get
            {
                if(_ClipboardCopyItem == null)
                {
                    _ClipboardCopyItem = new RelayCommand(
                        o =>
                        {
                            string item = o as string;
                            copyToClipboard(item);
                        });
                }
                return _ClipboardCopyItem;
            }
        }

        #endregion

        #endregion
    }
}
