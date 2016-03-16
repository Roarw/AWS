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
                var p = new Player
                {
                    ID = 3,
                    Buffs = 111,
                    Funds = 111,
                    Score = 111
                };

                con.UpdateRow(p);
            }

            using (var game = new GameWorld())
                game.Run();
        }
    }
#endif
}
