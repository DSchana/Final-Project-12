using Artemis.Engine;
using Half_Life_3.Entities.Characters;
using Half_Life_3.Entities;
using Half_Life_3.Menu;
using System;

namespace Half_Life_3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class Game1
    {
        public static PlayableCharacter Freeman { get; private set; }
        public static EntityManager EntManager { get; private set; }

        public Game1()
        {
            Freeman = new PlayableCharacter("Gordon Freeman");
            EntManager = new EntityManager();
            ArtemisEngine.RegisterMultiforms(new MenuMultiform("Main Menu"));
            ArtemisEngine.StartWith("Main Menu");
        }
    }
}
