
using System.Runtime.InteropServices;

namespace Project.Modules.ConsoleManager
{
    public static class ConsoleManager
    {
        // Импортируем функции из user32.dll и kernel32.dll
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();


        public static void Show()
        {
            AllocConsole();
        }

        public static void Hide()
        {
            FreeConsole();
        }
    }
}
