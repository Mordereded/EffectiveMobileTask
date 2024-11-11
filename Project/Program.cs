

using Project.Modules.ConsoleManager;

namespace Project_test_task
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ConsoleManager.Show();
            ApplicationConfiguration.Initialize();
            Application.Run(new MainMenu());
        }
    }
}