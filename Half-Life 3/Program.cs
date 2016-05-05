using System;
using Artemis.Engine;

namespace Half_Life_3
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
        static void Main()
        {
            ArtemisEngine.Setup("game.setup", Initialize);
        }

        static void Initialize()
        {
            // Game initiallization routine
            /*
            using(var game = new Game1())
                game.Run();
                */
        }
    }
#endif
}
