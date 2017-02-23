using Microsoft.Practices.Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace allkeeper.ViewModel
{
    public partial class MainViewModel
    {

        #region ClipboardVM
        private void ClipboardConstructor()
        {
            SearchBarForeground = Brushes.LightGray;
            searchBarText = "Search";
            this.ClipboardUpdateCommand = new DelegateCommand(OnClipboardUpdate, OnCanClipboardUpdate);
        }

        private Model.ClipboardModel clipboardModel = new Model.ClipboardModel();
        private ObservableCollection<string> ClipboardHistory { get; set; } = new ObservableCollection<string>();
        private ObservableCollection<string> SearchResult { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> ClipList
        {
            get
            {
                if (SearchResult.Count == 0 && (SearchBarText == "" || SearchBarText == "Search")) return ClipboardHistory;
                else return SearchResult;
            }
        }

        public void ClipboardModelSync()
        {
            ClipboardHistory = new ObservableCollection<string>(clipboardModel.HistoryList);
            SearchResult = new ObservableCollection<string>(clipboardModel.SearchResult);
            RaisePropertyChanged("ClipList");
        }
        
        private string selectedItem;
        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem == value)
                    return;
                selectedItem = value;
                if (selectedItem != null)
                    CopyToClipboard();
            }
        }

        private void CopyToClipboard()
        {
            clipboardModel.CopyToClipboard(SelectedItem);
        }

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

        private string searchBarText;
        public string SearchBarText
        {
            get { return searchBarText; }
            set
            {
                if (searchBarText == value)
                    return;
                searchBarText = value;
                RaisePropertyChanged("searchBarText");
                if (SearchBarText != "" || SearchBarText != "Search")
                    Search();
                else
                    SearchResult = new ObservableCollection<string>();
            }
        }

        private Brush _SearchBarForeground;
        public Brush SearchBarForeground
        {
            get { return _SearchBarForeground; }
            set
            {
                if (_SearchBarForeground == value) return;
                _SearchBarForeground = value;
                RaisePropertyChanged("SearchBarForeground");
            }
        }

        public void Search()
        {
            clipboardModel.Search(SearchBarText);
            ClipboardModelSync();
        }

        #endregion

        #region Commands

        private ICommand _SearchBarLeftButtonDown;
        public ICommand SearchBarLeftButtonDown
        {
            get
            {
                if (_SearchBarLeftButtonDown == null)
                    _SearchBarLeftButtonDown = new RelayCommand(
                        o =>
                        {
                            if(SearchBarText=="Search")
                            SearchBarText = "";
                            SearchBarForeground = Brushes.White;
                        });
                return _SearchBarLeftButtonDown;
            }
        }

        private ICommand _SearchBarLostFocus;
        public ICommand SearchBarLostFocus
        {
            get
            {
                if (_SearchBarLostFocus == null)
                    _SearchBarLostFocus = new RelayCommand(
                        o =>
                        {
                            if (SearchBarText == "")
                            {
                                SearchBarText = "Search";
                                SearchBarForeground = Brushes.LightGray;
                            }
                        });
                return _SearchBarLostFocus;
                
            }
        }

        private ICommand _ClearHistory;
        public ICommand ClearHistory
        {
            get
            {
                if (_ClearHistory == null)
                    _ClearHistory = new RelayCommand(
                        o =>
                        {
                            clipboardModel.ClearHistory();
                            SearchResult = new ObservableCollection<string>();
                            ClipboardModelSync();
                        });
                return _ClearHistory;
            }
        }

        private ICommand _DeleteHistoryItem;
        public ICommand DeleteHistoryItem
        {
            get
            {
                if (_DeleteHistoryItem == null)
                    _DeleteHistoryItem = new RelayCommand(
                        o =>
                        {
                            string item = (string)o;
                            clipboardModel.removeItem(item);
                            if (SearchResult.Contains(item))
                                SearchResult.Remove(item);
                            ClipboardModelSync();
                        });
                return _DeleteHistoryItem;
            }
        }

        #endregion

        #endregion
    }
}
