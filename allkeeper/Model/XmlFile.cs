using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace allkeeper.Model
{
    public class XmlFile
    {
        private static string path = "Data.xml";
        public static void saveNotes(List<Note> notes)
        {
            try
            {
                XDocument xml = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("Saving date: " + DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                    new XElement("Notes", from Note note in notes
                                          select new XElement("Note",
                                              new XElement("Title", note.Title),
                                              new XElement("Content", note.Content)
                                              )));
                xml.Save(path);
            }
            catch (Exception e)
            {
                throw new Exception("XML Saving File Error", e);
            }
        }

        public static List<Note> loadNotes()
        {
                try
                {
                    XDocument xml = XDocument.Load(path);
                    IEnumerable<Note> notesData =
                        from note in xml.Root.Descendants("Note")
                        select new Note(note.Element("Title").Value,
                            note.Element("Content").Value);
                    List<Note> notes = new List<Note>();
                    foreach (Note note in notesData) notes.Add(note);
                    return notes;
                }
                catch (Exception e)
                {
                    return new List<Note>();
                }
        }

    }
}
