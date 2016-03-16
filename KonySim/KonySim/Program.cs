using System;
using KonySim.Db;

namespace KonySim
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            using (var game = new GameWorld())
                game.Run();
        }
    }
#endif
}
