namespace JFTallerMaui;

public partial class JFNotePage : ContentPage
{
    string _fileName = Path.Combine(FileSystem.AppDataDirectory, "notes.txt");
    public JFNotePage()
    {
        InitializeComponent();
        if (File.Exists(_fileName))
            TextEditor.Text = File.ReadAllText(_fileName);

        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));
    }
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.JFNote note)
            File.WriteAllText(note.Filename, TextEditor.Text);

        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.JFNote note)
        {
            // Delete the file.
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }
    private void LoadNote(string fileName)
    {
        Models.JFNote noteModel = new Models.JFNote();
        noteModel.Filename = fileName;

        if (File.Exists(fileName))
        {
            noteModel.Date = File.GetCreationTime(fileName);
            noteModel.Text = File.ReadAllText(fileName);
        }

        BindingContext = noteModel;
    }
    public string ItemId
    {
        set { LoadNote(value); }

    }

    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NotePage : ContentPage { 
    }
}