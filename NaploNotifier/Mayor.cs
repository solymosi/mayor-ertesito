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
    static class Mayor
    {
        public static MayorSession Session = new MayorSession();
        public static Settings Settings = new Settings();

        public static List<Note> Notes = new List<Note>();
        public static List<NoteChange> Changes = new List<NoteChange>();

        public delegate void OsztalyozoUpdateDelegate(List<NoteChange> Changes);
        public static event OsztalyozoUpdateDelegate OsztalyozoUpdated = delegate { };

        public static bool OsztalyozoUpdating = false;

        public static string SettingsDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\MayorNotify";

        public static void UpdateOsztalyozo() { UpdateOsztalyozo(false); }
        public static void UpdateOsztalyozo(bool Initial)
        {
            if (OsztalyozoUpdating) { return; }

            OsztalyozoUpdating = true;

            try
            {
                HtmlDocument doc = Mayor.Session.DoRequest("naplo", "osztalyozo", "diak", "", "private", "POST", "tolDt=2000-01-01");

                List<Subject> subjects = new List<Subject>();
                List<Note> notes = new List<Note>();
                HtmlNode table = doc.DocumentNode.SelectSingleNode("//table[@class='osztalyozo']");
                foreach (HtmlNode subject in SelectNodes(table, "tbody/tr"))
                {
                    Subject s = Subject.Parse(subject);
                    subjects.Add(s);
                    notes.AddRange(s.Notes);
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

                List<NoteChange> changes = new List<NoteChange>();

                if (!Initial)
                {
                    // Check for deleted notes
                    foreach (Note n in Notes)
                    {
                        if (!notes.Contains<Note>(n, new Comparer<Note>(new Func<Note, Note, bool>(delegate(Note n1, Note n2)
                        {
                            return n1.Id == n2.Id;
                        }))))
                        {
                            changes.Add(new NoteChange(n, null));
                        }
                    }

                    // Check for added and modified notes
                    foreach (Note n in notes)
                    {
                        if (!Notes.Contains<Note>(n, new Comparer<Note>(new Func<Note, Note, bool>(delegate(Note n1, Note n2)
                        {
                            return n1.Id == n2.Id;
                        }))))
                        {
                            changes.Add(new NoteChange(null, n));
                            continue;
                        }

                        foreach (Note n2 in Notes)
                        {
                            if (n2.Id == n.Id && !n.Equals(n2))
                            {
                                changes.Add(new NoteChange(n2, n));
                                break;
                            }
                        }
                    }

                    Changes.AddRange(changes);
                    SaveChanges();
                    OsztalyozoUpdated(changes);
                }

                Notes = notes;
                SaveOsztalyozo();
            }
            catch (LoginFailedException)
            {
                System.Windows.Forms.MessageBox.Show("Nem sikerült ellenőrizni a jegyedidet, mert a megadott felhasználónév vagy jelszó hibás. Kattints jobb gombbal az óra melletti MaYoR ikonra, és a Beállítások menüpontban adj meg egy érvényes felhasználónév/jelszó párost.", System.Windows.Forms.Application.ProductName, 0, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Nem sikerült ellenőrizni a jegyedidet. Győződj meg arról, hogy van aktív internetkapcsolat és megfelelő a Beállítások menüpontban megadott webes cím.", System.Windows.Forms.Application.ProductName, 0, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally { OsztalyozoUpdating = false; }
        }

        public static void SaveSettings()
        {
            SerializeToFile<Settings>(Settings, "Settings.data");
        }

        public static void LoadSettings()
        {
            Settings = DeserializeFromFile<Settings>("Settings.data");
        }

        public static void SaveOsztalyozo()
        {
            SerializeToFile<List<Note>>(Notes, "Osztalyozo.data");
        }

        public static void LoadOsztalyozo()
        {
            Notes = DeserializeFromFile<List<Note>>("Osztalyozo.data");
        }

        public static void SaveChanges()
        {
            SortChanges();
            if (Changes.Count > 20)
            {
                Changes = Changes.GetRange(0, 20);
            }
            SerializeToFile<List<NoteChange>>(Changes, "Valtozasok.data");
        }

        public static void SortChanges()
        {
            Changes.Sort(new Comparison<NoteChange>(delegate(NoteChange one, NoteChange two)
            {
                if (one.Date == two.Date) { return 0; }
                if (one.Date > two.Date) { return -1; }
                return 1;
            }));
        }

        public static void LoadChanges()
        {
            Changes = DeserializeFromFile<List<NoteChange>>("Valtozasok.data");
        }

        public static void SerializeToFile<T>(T obj, string Path)
        {
            CreateSettingsDirectory();
            using (Stream stream = File.Open(SettingsDir + "\\" + Path, FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, obj);
            }
        }

        public static T DeserializeFromFile<T>(string Path)
        {
             CreateSettingsDirectory();
            using (Stream stream = File.Open(SettingsDir + "\\" + Path, FileMode.Open))
            {
                BinaryFormatter bin = new BinaryFormatter();
                return (T)bin.Deserialize(stream);
            }
        }

        public static void CreateSettingsDirectory()
        {
            if (!Directory.Exists(SettingsDir)) { Directory.CreateDirectory(SettingsDir); }
        }

        public static HtmlNodeCollection SelectNodes(HtmlNode Base, string XPath)
        {
            HtmlNodeCollection collection = Base.SelectNodes(XPath);
            if (collection == null) { collection = new HtmlNodeCollection(Base); }
            return collection;
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

        internal static void RunUpdate()
        {
            throw new NotImplementedException();
        }

        internal static void RunUpdate(object Parameter)
        {
            throw new NotImplementedException();
        }
    }

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
    }

    [Serializable()]
    public class NoteChange
    {
        public Note OldNote;
        public Note NewNote;
        public DateTime Date;
        public ChangeType Type
        {
            get
            {
                if (OldNote == null) { return ChangeType.Added; }
                if (NewNote == null) { return ChangeType.Deleted; }
                return ChangeType.Modified;
            }
        }

        public NoteChange(Note old, Note newnote)
        {
            this.OldNote = old;
            this.NewNote = newnote;
            this.Date = DateTime.Now;
        }

        public override string ToString()
        {
            switch (this.Type)
            {
                case ChangeType.Added: return this.Date.ToString() + "\r\nÚJ JEGYET KAPTÁL:\r\n" + this.NewNote.BalloonDescription();
                case ChangeType.Deleted: return this.Date.ToString() + "\r\nTÖRÖLVE LETT EGY JEGYED:\r\n" + this.OldNote.BalloonDescription();
                case ChangeType.Modified: return this.Date.ToString() + "\r\nMEGVÁLTOZOTT EGY JEGYED:\r\n" + this.OldNote.BalloonDescription() + "\r\nÚJ JEGY:\r\n" + this.NewNote.BalloonDescription();
            }
            return "";
        }

        public string Text()
        {
            switch (this.Type)
            {
                case ChangeType.Added: return this.NewNote.BalloonDescription();
                case ChangeType.Deleted: return this.OldNote.BalloonDescription();
                case ChangeType.Modified: return this.OldNote.BalloonDescription() + "\r\n==>\r\n" + this.NewNote.BalloonDescription();
            }
            return "";
        }

        public string Title()
        {
            switch (this.Type)
            {
                case ChangeType.Added: return "Új " + this.NewNote.Subject.Name + " jegyet kaptál";
                case ChangeType.Deleted: return "Törölve lett egy " + this.OldNote.Subject.Name + " jegyed";
                case ChangeType.Modified: return "Megváltozott egy " + this.NewNote.Subject.Name + " jegyed";
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

    public enum ChangeType { Added, Deleted, Modified }

    [Serializable()]
    public class Settings
    {
        public string Domain = "";
        public string User = "";
        public string EncryptedPassword = "";
        public string BaseAddress { get { return "https://" + this.Domain + "/index.php"; } }
        public string Password
        {
            get { return Encoding.UTF8.GetString(Convert.FromBase64String(EncryptedPassword)); }
            set { EncryptedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(value)); }
        }
        public bool CheckAutomatically = true;
    }

    class Comparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _comparer;

        public Comparer(Func<T, T, bool> comparer)
        {
            if (comparer == null)
                throw new ArgumentNullException("comparer");

            _comparer = comparer;
        }

        public bool Equals(T x, T y)
        {
            return _comparer(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.ToString().ToLower().GetHashCode();
        }
    }

}