using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaploNotifier
{
    [Serializable()]
    public class Change
    {
        public Note Old;
        public Note New;
        public DateTime Date;
        public string Title
        {
            get
            {
                switch (this.Type)
                {
                    case ChangeType.Added: return "Új " + this.New.Subject.Name + " jegyet kaptál";
                    case ChangeType.Deleted: return "Törölve lett egy " + this.New.Subject.Name + " jegyed";
                    case ChangeType.Modified: return "Megváltozott egy " + this.New.Subject.Name + " jegyed";
                }
                throw new ArgumentException();
            }
        }
        public ChangeType Type
        {
            get
            {
                if (Old == null) { return ChangeType.Added; }
                if (New == null) { return ChangeType.Deleted; }
                return ChangeType.Modified;
            }
        }

        public Change(Note Old, Note New)
        {
            this.Old = Old;
            this.New = New;
            this.Date = DateTime.Now;
        }
    }

    public enum ChangeType { Added, Deleted, Modified }
}
