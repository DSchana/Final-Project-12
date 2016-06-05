namespace Half_Life_3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class Game1
    {
        public PlayableCharacter Freeman { get; private set; }
        public static CharacterManager CharManager { get; private set; }

        public Game1()
        {
            Freeman = new PlayableCharacter("Gordon Freeman", "Something.aaml");
            CharManager = new CharacterManager();
        }

        public void Run()
        {
            // Rendering
            Freeman.Render();
        }
    }
}
