namespace JFTallerMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.JFNotePage), typeof(Views.JFNotePage));

        }
    }
}
