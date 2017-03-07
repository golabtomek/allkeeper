using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allkeeper.Model
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

    public class Notes
    {
        public List<Note> notes = new List<Note>();

        public void addItem(string title, string content)
        {
            Note note = new Note(title, content);
            notes.Add(note);
        }

        public void addItem(Note note)
        {
            notes.Add(note);
        }

        public void removeItem(string title, string content)
        {
            foreach(Note note in notes)
            {
                if(note.content == content && note.title == title)
                {
                    notes.Remove(note);
                    break;
                }
            }
        }

        public void removeItem(Note note)
        {
            if (notes.Contains(note))
                notes.Remove(note);
        }

        public void editItem(string oldTitle, string oldContent, string newTitle, string newContent)
        {
            Note oldNote = new Note(oldTitle, oldContent);
            Note newNote = new Note(newTitle, newContent);
            if(oldNote != newNote)
            {
                foreach(Note note in notes)
                {
                    if(note.content == oldNote.content && note.title == oldNote.title)
                    {
                        notes.Remove(note);
                        break;
                    }
                }
                notes.Add(newNote);
            }
        }

        public void editItem(Note oldNote, Note newNote)
        {
            if (oldNote != newNote)
            {
                foreach (Note note in notes)
                {
                    if (note.content == oldNote.content && note.title == oldNote.title)
                    {
                        notes.Remove(note);
                        break;
                    }
                }
                notes.Add(newNote);
            }
        }

        public void saveData()
        {
            XmlFiles.saveNotes(notes);
        }

        public void loadData()
        {
            notes = XmlFiles.loadNotes();
        }
    }
}
