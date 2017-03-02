using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace allkeeper.Model
{
    public class Note
    {
        public string title { get; set; }
        public string content { get; set; }

        public Note(string title, string content)
        {
            this.title = title;
            this.content = content;
        }
    }

    public class NotesModel
    {
        public List<Note> Notes = new List<Note>();
        

        public void addItem(string Title, string Content)
        {
            Notes.Add(new Note(Title, Content));
        }

        public void removeItem(string Title, string Content)
        {
            Note note = new Note(Title, Content);
            if (Notes.Contains(note))
                Notes.Remove(note);
        }

        public void removeItem(Note note)
        {
            foreach (Note item in Notes)
            {
                if (item == note)
                {
                    Notes.Remove(item);
                    break;
                }
            }
        }
        
        public void editItem(string OldTitle, string OldContent, string NewTitle, string NewContent)
        {
            Note OldItem = new Note(OldTitle, OldContent);
            Note NewItem = new Note(NewTitle, NewContent);
            if (OldItem != NewItem)
            {
                foreach(Note item in Notes)
                {
                    if (item.content == OldItem.content && item.title == OldItem.title)
                    {
                        Notes.Remove(item);
                        Notes.Add(NewItem);
                        break;
                    }
                }
            }
            
        }

        public void editItem(Note OldItem, Note NewItem)
        {
            if (OldItem != NewItem)
            {
                foreach (Note item in Notes)
                {
                    if (item == NewItem)
                    {
                        Notes.Remove(item);
                        Notes.Add(NewItem);
                    }
                }
            }
        }

        public void saveData()
        {
            XmlFile.saveNotes(Notes);
        }

        public void loadData()
        {
            List<Note> notes = XmlFile.loadNotes();
            Notes = notes;
        }
    }
}
