using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace NaploNotifier
{
    [Serializable()]
    public class Note
    {
        public int ID;
        public string Grade;
        public string Name;
        public NoteType Type;
        public string FriendlyType
        {
            get
            {
                switch (Type)
                {
                    case NoteType.Kicsi: return "Kis jegy";
                    case NoteType.Normal: return "Normál jegy";
                    case NoteType.Dolgozat: return "Dolgozat";
                    case NoteType.Temazaro: return "Témazáró";
                    case NoteType.Vizsga: return "Vizsgajegy";
                }
                throw new ArgumentException();
            }
        }
        public Subject Subject;

        public Note(int ID, string Grade, string Name, NoteType Type)
        {
            this.ID = ID;
            this.Grade = Grade;
            this.Name = Name;
            this.Type = Type;
        }

        public static Note Parse(HtmlNode Node)
        {
            string Grade = Node.InnerText.Trim();
            string Name = Node.GetAttributeValue("title", "Névtelen jegy");
            string Href = Node.GetAttributeValue("href", "");
            Match Match = Regex.Match(Href, "jegyId=(?<id>[0-9]+)", RegexOptions.IgnoreCase);
            int ID = int.Parse(Match.Groups["id"].Value);
            string T = Node.GetAttributeValue("class", "jegy2");
            NoteType Type = (NoteType)(int.Parse(T.Substring(T.Length - 1, 1)));
            return new Note(ID, Grade, Name, Type);
        }

        public bool Equals(Note Other)
        {
            return (Other.ID == this.ID && Other.Grade == this.Grade && Other.Type == this.Type);
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
