using System.Collections.Generic;
using System.Windows;

namespace allkeeper.Model
{
    public class ClipboardModel
    {
        private List<string> HistoryList = new List<string>();
        private List<string> SearchResult = new List<string>();
        private string searchText = "";

        public List<string> get()
        {
            if (searchText == "" || searchText == "Search") return HistoryList;
            else return SearchResult;
        }

        public void addItem()
        {
            string text = Clipboard.GetText();
            if (text != null && text != "" && text != " " && CheckHistory(text) != true)
                HistoryList.Add(text);
        }

        public void removeItem(string text)
        {
            HistoryList.Remove(text);
            if (SearchResult.Contains(text))
                SearchResult.Remove(text);
        }

        public bool CheckHistory(string text)
        {
            bool isTextAlreadyInHistory = false;
            foreach (string t in HistoryList)
            {
                if (text == t) isTextAlreadyInHistory = true;
            }
            return isTextAlreadyInHistory;
        }
        
        public void CopyToClipboard(string text)
        {
            Clipboard.SetText(text);
        }

        public void Clear()
        {
            HistoryList = new List<string>();
            SearchResult = new List<string>();
        }


        public void Search(string text)
        {
            SearchResult = new List<string>();
            searchText = text;
            if (text != "" || text != "Search")
            {
                foreach (string item in HistoryList)
                {
                    string item_downcase = item.ToLower();
                    if (item_downcase.Contains(text.ToLower()))
                        SearchResult.Add(item);
                }
            }
        }
    }
}
