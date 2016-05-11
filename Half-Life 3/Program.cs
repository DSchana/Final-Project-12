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
            ArtemisEngine.Setup("game.setup", Setup);
            ArtemisEngine.Begin(Initialize);
        }

        static void Setup()
        {

        }

        static void Initialize()
        {
            Console.WriteLine("Half-Life 3");
        }
    }
#endif
}
