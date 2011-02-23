using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NaploNotifier
{
    static partial class Mayor
    {
        public static Session Session = new Session();
        public static Settings Settings = new Settings();

        public static List<Note> Notes = new List<Note>();
        public static List<Change> Changes = new List<Change>();

        public const int DefaultRecentChangeCount = 5;
        public static List<Change> RecentChanges
        {
            get
            {
                SortChanges();
                return Mayor.Changes.GetRange(0, Math.Min(DefaultRecentChangeCount, Mayor.Changes.Count));
            }
        }

        public delegate void UpdateDelegate(List<Change> Changes);
        public static event UpdateDelegate Updated = delegate { };

        public static void RunUpdate() { RunUpdate(false); }
        public static void RunUpdate(bool InitialUpdate)
        {
            List<Subject> NewSubjects = new List<Subject>();
            List<Note> NewNotes = new List<Note>();

            HtmlDocument Document = Mayor.Session.DoRequest("naplo", "osztalyozo", "diak", "", "private", "POST", "tolDt=2000-01-01");
            HtmlNode Table = Document.DocumentNode.SelectSingleNode("//table[@class='osztalyozo']");

            foreach (HtmlNode CurrentSubject in SelectNodes(Table, "tbody/tr"))
            {
                Subject Subject = Subject.Parse(CurrentSubject);
                NewSubjects.Add(Subject);
                NewNotes.AddRange(Subject.Notes);
            }

            /*int x = new Random().Next(1, 3);
                
            if (x == 1)
            {
                notes.RemoveAt(0);
                Note n = new Note(11111, "4/5", "TESZT", NoteType.Kicsi);
                n.Subject = subjects[3];
                notes.Add(n);
                notes[1].Grade = "3/4";
            }*/

            if (!InitialUpdate)
            {
                CheckChanges(NewNotes);
            }

            Notes = NewNotes;
            SaveNotes();
        }

        public static void CheckChanges(List<Note> NewNotes)
        {
            List<Change> NewChanges = new List<Change>();

            foreach (Note CurrentNote in Notes)
            {
                if (NewNotes.Where(new Func<Note, bool>(delegate(Note N) { return CurrentNote.Id == N.Id; })).ToList().Count == 0)
                {
                    NewChanges.Add(new Change(CurrentNote, null));
                }
            }

            foreach (Note CurrentNote in NewNotes)
            {
                if (Notes.Where(new Func<Note, bool>(delegate(Note N) { return CurrentNote.Id == N.Id; })).ToList().Count == 0)
                {
                    NewChanges.Add(new Change(null, CurrentNote));
                }
            }

            foreach (Note CurrentNote in NewNotes)
            {
                foreach (Note OldNote in Notes)
                {
                    if (CurrentNote.Id == OldNote.Id && !CurrentNote.Equals(OldNote))
                    {
                        NewChanges.Add(new Change(OldNote, CurrentNote));
                        break;
                    }
                }
            }

            Changes.AddRange(NewChanges);
            SaveChanges();

            Updated(NewChanges);
        }

        public static HtmlNodeCollection SelectNodes(HtmlNode Base, string XPath)
        {
            HtmlNodeCollection Collection = Base.SelectNodes(XPath);
            if (Collection == null) { Collection = new HtmlNodeCollection(Base); }
            return Collection;
        }

        public static void SortChanges()
        {
            Changes.Sort(new Comparison<Change>(delegate(Change First, Change Second)
            {
                if (First.Date > Second.Date) { return -1; }
                if (First.Date == Second.Date) { return 0; }
                if (First.Date < Second.Date) { return 1; }
                throw new ArgumentException();
            }));
        }
    }
}