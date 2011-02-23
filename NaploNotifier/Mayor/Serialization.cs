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
            Tools.Serialize<Settings>(Settings, SettingsFileName);
        }

        public static void LoadSettings()
        {
            Settings = Tools.Deserialize<Settings>(SettingsFileName);
        }

        public static void SaveNotes()
        {
            Tools.Serialize<List<Note>>(Notes, NotesFileName);
        }

        public static void LoadNotes()
        {
            Notes = Tools.Deserialize<List<Note>>(NotesFileName);
        }

        public static void SaveChanges()
        {
            Tools.Serialize<List<Change>>(RecentChanges, ChangesFileName);
        }

        public static void LoadChanges()
        {
            Changes = Tools.Deserialize<List<Change>>(ChangesFileName);
        }

    }
}
