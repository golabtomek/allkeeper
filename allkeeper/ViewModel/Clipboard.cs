using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Allkeeper.ViewModel
{
    public class Clipboard : INotifyPropertyChanged
    {
        private ObservableCollection<string> history = new ObservableCollection<string>();
        private ObservableCollection<string> searchResult = new ObservableCollection<string>();
        private Model.ClipboardModel model = new Model.ClipboardModel();

        public ObservableCollection<string> clipboard
        {
            get
            {
                if (searchBarText != "" && searchBarText != "Search")
                    return searchResult;
                return history;
            }
        }

        private string _searchBarText;
        public string searchBarText
        {
            get { return _searchBarText; }
            set
            {
                if (_searchBarText == value) return;
                _searchBarText = value;
                RaisePropertyChanged("searchBarText");
                search();
            }
        }

        private Brush _searchBarForeground;
        public Brush searchBarForeground
        {
            get { return _searchBarForeground; }
            set
            {
                if (_searchBarForeground == value) return;
                _searchBarForeground = value;
                RaisePropertyChanged("searchBarForeground");
            }
        }

        public Clipboard()
        {
            ClipboardUpdateCommand = new DelegateCommand(OnClipboardUpdate, OnCanClipboardUpdate);
            searchBarText = "Search";
            searchBarForeground = Brushes.LightGray;
        }
        
        private void modelSync()
        {
            history = new ObservableCollection<string>(model.history);
            RaisePropertyChanged("clipboard");
        }


        private void search()
        {
            if (searchBarText == "" || searchBarText == "Search")
            {
                modelSync();
                RaisePropertyChanged("clipboard");
            }
            else
            {
                List<string> result = model.searchInHistory(searchBarText);
                searchResult = new ObservableCollection<string>(result);
                RaisePropertyChanged("clipboard");
            }
        }

        #region Commands
        private ICommand _clearHistory;
        public ICommand clearHistory
        {
            get
            {
                if (_clearHistory == null)
                {
                    _clearHistory = new RelayCommand(
                        o =>
                        {
                            model.clearHistory();
                            modelSync();
                        });
                }
                return _clearHistory;
            }
        }

        private ICommand _removeItem;
        public ICommand removeItem
        {
            get
            {
                if(_removeItem == null)
                {
                    _removeItem = new RelayCommand(
                        o =>
                        {
                            string item = o as string;
                            model.removeItem(item);
                            modelSync();
                        });
                }
                return _removeItem;
            }
        }

        private ICommand _copyItem;
        public ICommand copyItem
        {
            get
            {
                if(_copyItem == null)
                {
                    _copyItem = new RelayCommand(
                        o =>
                        {
                            string item = o as string;
                            model.copyHistoryItemToUserClipboard(item);
                        });
                }
                return _copyItem;
            }
        }

        private ICommand _searchBarLeftButtonDown;
        public ICommand searchBarLeftButtonDown
        {
            get
            {
                if (_searchBarLeftButtonDown == null)
                    _searchBarLeftButtonDown = new RelayCommand(
                        o =>
                        {
                            if (searchBarText == "Search")
                                searchBarText = "";
                            searchBarForeground = Brushes.White;
                        });
                return _searchBarLeftButtonDown;
            }
        }

        private ICommand _searchBarLostFocus;
        public ICommand searchBarLostFocus
        {
            get
            {
                if (_searchBarLostFocus == null)
                    _searchBarLostFocus = new RelayCommand(
                        o =>
                        {
                            if (searchBarText == "")
                            {
                                searchBarText = "Search";
                                searchBarForeground = Brushes.LightGray;
                            }
                        });
                return _searchBarLostFocus;

            }
        }
        #endregion

        public DelegateCommand ClipboardUpdateCommand { get; private set; }
        public void OnClipboardUpdate()
        {
            model.addItem();
            modelSync();
        }
        public bool OnCanClipboardUpdate()
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
