using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace NaploNotifier
{
    [Serializable()]
    public class Note
    {
        public int Id;
        public string Grade;
        public string Name;
        public NoteType Type;
        public Subject Subject;

        public Note(int Id, string Grade, string Name, NoteType Type)
        {
            this.Id = Id;
            this.Grade = Grade;
            this.Name = Name;
            this.Type = Type;
        }

        public static Note Parse(HtmlNode Node)
        {
            string grade = Node.InnerText.Trim();
            string name = Node.GetAttributeValue("title", "Névtelen jegy");
            string href = Node.GetAttributeValue("href", "");
            Match m = Regex.Match(href, "jegyId=(?<id>[0-9]+)", RegexOptions.IgnoreCase);
            int id = int.Parse(m.Groups["id"].Value);
            string tp = Node.GetAttributeValue("class", "jegy2");
            NoteType type = (NoteType)(int.Parse(tp.Substring(tp.Length - 1, 1)));
            return new Note(id, grade, name, type);
        }

        public override string ToString()
        {
            return "[" + Grade.ToString() + "] " + Name + " (" + Type.ToString() + ") [" + Id.ToString() + "]";
        }

        public bool Equals(Note obj)
        {
            return (obj.Id == this.Id && obj.Grade == this.Grade && obj.Type == this.Type);
        }

        public string BalloonDescription()
        {
            return this.Name + "\r\nOsztályzat: " + this.Grade + "\r\nTípus: " + Mayor.FriendlyNoteTypeName(this.Type);
        }

        public static string FriendlyNoteTypeName(NoteType type)
        {
            switch (type)
            {
                case NoteType.Kicsi: return "Kis jegy";
                case NoteType.Normal: return "Normál jegy";
                case NoteType.Dolgozat: return "Dolgozat";
                case NoteType.Temazaro: return "Témazáró";
                case NoteType.Vizsga: return "Vizsgajegy";
            }
            return "";
        }
    }

    public enum NoteType
    {
        Kicsi = 1,
        Normal = 2,
        Dolgozat = 3,
        Temazaro = 4,
        Vizsga = 5
    }
}
