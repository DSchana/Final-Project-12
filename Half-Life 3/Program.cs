using System;
using Artemis.Engine;
using Artemis.Engine.Input;

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
            ArtemisEngine.Setup("game.constants", Setup);
            ArtemisEngine.Begin(Initialize);
        }

        static void Setup()
        {

        }

        static void Initialize()
        {
            ArtemisEngine.DisplayManager.SetWindowTitle("Half-Life 3");
            Run();
        }

        /// <summary>
        /// Run the game
        /// </summary>
        static void Run()
        {
            Game1 game = new Game1();
        }
    }
#endif
}
