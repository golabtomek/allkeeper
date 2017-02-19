using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace allkeeper.ViewModel
{
    public partial class MainViewModel
    {

        #region ClipboardVM
        private ObservableCollection<string> ClipboardHistory { get; set; } = new ObservableCollection<string>();
        private ObservableCollection<string> SearchResult { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<string> List
        {
            get
            {
                if (SearchResult.Count == 0 && (SearchBarText == "" || SearchBarText == "Search")) return ClipboardHistory;
                else return SearchResult;
            }
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
            Clipboard.SetText(SelectedItem);
        }

        #region clipboardupdate
        public DelegateCommand ClipboardUpdateCommand { get; private set; }

        public bool CheckHistory(string text)
        {
            bool isTextAlreadyInHistory = false;
            foreach (string t in ClipboardHistory)
            {
                if (text == t) isTextAlreadyInHistory = true;
            }
            return isTextAlreadyInHistory;
        }

        public void OnClipboardUpdate()
        {
            string text = Clipboard.GetText();
            if (text != null && text != "" && text != " " && CheckHistory(text) != true)
                ClipboardHistory.Add(text);
        }


        public bool OnCanClipboardUpdate()
        {
            return true;
        }

        private string searchBarText = "Search";
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

        public void Search()
        {
            SearchResult = new ObservableCollection<string>();
            foreach (string item in ClipboardHistory)
            {
                string item_downcase = item.ToLower();
                if (item_downcase.Contains(SearchBarText.ToLower()))
                    SearchResult.Add(item);
            }
            RaisePropertyChanged("List");
        }

        #endregion

        #region Commands

        private ICommand _ClearHistory;
        public ICommand ClearHistory
        {
            get
            {
                if (_ClearHistory == null)
                    _ClearHistory = new RelayCommand(
                        o =>
                        {
                            ClipboardHistory = new ObservableCollection<string>();
                            SearchResult = new ObservableCollection<string>();
                            RaisePropertyChanged("List");
                        });
                return _ClearHistory;
            }
        }

        #endregion

        #endregion
    }
}
