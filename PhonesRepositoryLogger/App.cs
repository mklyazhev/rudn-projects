using System.Windows;

namespace PhonesRepositoryLogger
{
    public class App : Application
    {
        private readonly MainWindow mainWindow;
        public App(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
