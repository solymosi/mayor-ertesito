using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;

namespace NaploNotifier
{
    [Serializable()]
    public class Subject
    {
        public List<Note> Notes = new List<Note>();
        public string Name;

        public Subject(string Name)
        {
            this.Name = Name;
        }

        public static Subject Parse(HtmlNode Node)
        {
            Subject Subject = new Subject(Node.SelectSingleNode("th").InnerText);
            foreach (HtmlNode NoteNode in Mayor.SelectNodes(Node, "td/a"))
            {
                try
                {
                    Note Note = Note.Parse(NoteNode);
                    Note.Subject = Subject;
                    Subject.Notes.Add(Note);
                }
                catch { }
            }
            return Subject;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
