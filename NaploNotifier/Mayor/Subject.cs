using System;
using System.Collections.Generic;
using System.Linq;
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
            Subject s = new Subject(Node.SelectSingleNode("th").InnerText);
            foreach (HtmlNode note in Mayor.SelectNodes(Node, "td/a"))
            {
                try
                {
                    Note n = Note.Parse(note);
                    n.Subject = s;
                    s.Notes.Add(n);
                }
                catch { }
            }
            return s;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
