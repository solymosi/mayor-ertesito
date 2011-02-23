using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaploNotifier
{
    [Serializable()]
    public class Change
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

        public Change(Note old, Note newnote)
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

    public enum ChangeType { Added, Deleted, Modified }
}
