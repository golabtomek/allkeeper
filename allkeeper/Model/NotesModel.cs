using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace allkeeper.Model
{
    public class Note
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Note(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }

    public class NotesModel
    {
        public List<Note> NotesList = new List<Note>();
        

        public void addItem(string Title, string Content)
        {
            NotesList.Add(new Note(Title, Content));
        }

        public void removeItem(string Title, string Content)
        {
            Note note = new Note(Title, Content);
            if (NotesList.Contains(note))
                NotesList.Remove(note);
        }

        public void removeItem(Note note)
        {
            foreach (Note item in NotesList)
            {
                if (item == note)
                {
                    NotesList.Remove(item);
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
                foreach(Note item in NotesList)
                {
                    if (item.Content == OldItem.Content && item.Title == OldItem.Title)
                    {
                        NotesList.Remove(item);
                        NotesList.Add(NewItem);
                        break;
                    }
                }
            }
            
        }

        public void editItem(Note OldItem, Note NewItem)
        {
            if (OldItem != NewItem)
            {
                foreach (Note item in NotesList)
                {
                    if (item == NewItem)
                    {
                        NotesList.Remove(item);
                        NotesList.Add(NewItem);
                    }
                }
            }
        }

        public void saveData()
        {
            XmlFile.saveNotes(NotesList);
        }

        public void loadData()
        {
            List<Note> notes = XmlFile.loadNotes();
            NotesList = notes;
        }
    }
}
