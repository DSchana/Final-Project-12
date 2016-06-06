using Half_Life_3.Characters;
using Artemis.Engine;

namespace Half_Life_3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class Game1
    {
        public static PlayableCharacter Freeman { get; private set; }
        public static CharacterManager CharManager { get; private set; }

        public Game1()
        {
            Freeman = new PlayableCharacter("Gordon Freeman", "Something.aaml");
            CharManager = new CharacterManager();
            ArtemisEngine.RegisterMultiforms(new MenuMultiform("Main Menu"));
            ArtemisEngine.StartWith("Main Menu");
        }

        public void Run()
        {
            // Rendering
            Freeman.Render();
        }
    }
}
