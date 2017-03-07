using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Allkeeper.Model
{
    public class XmlFiles
    {
        private static string notesDataPath = "notesData.xml";
        private static string settingsPath = "settings.txt";

        public static void saveNotes(List<Note> notes)
        {
            try
            {
                XDocument xml = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XComment("Saving date: " + DateTime.Now.ToString(CultureInfo.InvariantCulture)),
                    new XElement("Notes", from Note note in notes
                                          select new XElement("Note",
                                              new XElement("Title", note.title),
                                              new XElement("Content", note.content)
                                              )));
                xml.Save(notesDataPath);
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
                XDocument xml = XDocument.Load(notesDataPath);
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


        public static void saveWindowSettings(string position)
        {
            try
            {
                StreamWriter file = new StreamWriter(settingsPath);
                file.WriteLine(position);
                file.Close();
            }
            catch
            {
                throw new Exception("Error saving to txt");
            }
        }

        public static string loadWindowSettings()
        {
            string position = "";
            try
            {
                StreamReader file = new StreamReader(settingsPath);
                position = file.ReadLine();
                return position;
            }
            catch
            {
                return position;
            }
        }

    }
}
