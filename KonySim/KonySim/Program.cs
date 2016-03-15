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
            //Db connection testing
            using (Connection con = new Connection())
            {
                var p = con.GetRow<Player>(1);
            }

            using (var game = new GameWorld())
                game.Run();
        }
    }
#endif
}
