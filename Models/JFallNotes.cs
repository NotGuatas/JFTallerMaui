using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JFTallerMaui.Models
{
    internal class JFallNotes
    {
        public ObservableCollection<JFNote> Notes { get; set; } = new ObservableCollection<JFNote>();

        public JFallNotes() =>
            LoadNotes();

        public void LoadNotes()
        {
            Notes.Clear();

            // Get the folder where the notes are stored.
            string appDataPath = FileSystem.AppDataDirectory;

            // Use Linq extensions to load the *.notes.txt files.
            IEnumerable<JFNote> notes = Directory

                                        // Select the file names from the directory
                                        .EnumerateFiles(appDataPath, "*.notes.txt")

                                        // Each file name is used to create a new Note
                                        .Select(filename => new JFNote()
                                        {
                                            Filename = filename,
                                            Text = File.ReadAllText(filename),
                                            Date = File.GetLastWriteTime(filename)
                                        })

                                        // With the final collection of notes, order them by date
                                        .OrderBy(note => note.Date);

            // Add each note into the ObservableCollection
            foreach (JFNote note in notes)
                Notes.Add(note);
        }
    }
}
