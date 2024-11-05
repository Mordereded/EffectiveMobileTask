

namespace Project_test_task
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ConsoleManager.ConsoleManager.Show();
            ApplicationConfiguration.Initialize();
            Application.Run(new MainMenu());
        }
    }
}