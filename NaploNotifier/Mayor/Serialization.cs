using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaploNotifier
{
    public static partial class Mayor
    {
        public const string SettingsFileName = "Settings.data";
        public const string NotesFileName = "Notes.data";
        public const string ChangesFileName = "Changes.data";

        public static void SaveSettings()
        {
            SerializeToFile<Settings>(Settings, "Settings.data");
        }

        public static void LoadSettings()
        {
            Settings = DeserializeFromFile<Settings>("Settings.data");
        }

        public static void SaveNotes()
        {
            SerializeToFile<List<Note>>(Notes, "Osztalyozo.data");
        }

        public static void LoadNotes()
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



        public static void LoadChanges()
        {
            Changes = DeserializeFromFile<List<NoteChange>>("Valtozasok.data");
        }

    }
}
