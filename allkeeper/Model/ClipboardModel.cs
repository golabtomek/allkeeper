using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Allkeeper.Model
{
    public class ClipboardModel
    {
        public List<string> history = new List<string>();

        public void addItem()
        {
            string clipboardItem = Clipboard.GetText();
            if (clipboardItem != "" && !history.Contains(clipboardItem) && clipboardItem != null)
                history.Add(clipboardItem);
        }
        
        public void removeItem(string clipboardItem)
        {
            if (history.Contains(clipboardItem))
                history.Remove(clipboardItem);
        }

        public void copyHistoryItemToUserClipboard(string clipboardItem)
        {
            Clipboard.SetText(clipboardItem);
        }

        public void clearHistory()
        {
            history = new List<string>();
        }

        public List<string> searchInHistory(string searchQuery)
        {
            List<string> searchResult = new List<string>();
            foreach(string clipboardItem in history)
            {
                string clipboardItemDowncase = clipboardItem.ToLower();
                if ( clipboardItemDowncase.Contains(searchQuery.ToLower()) )
                {
                    searchResult.Add(clipboardItem);
                }
            }
            return searchResult;
        }
    }
}
