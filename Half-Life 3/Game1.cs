using Artemis.Engine;
using Artemis.Engine.Assets;
using Half_Life_3.Entities.Characters;
using Half_Life_3.Entities;
using Half_Life_3.Story;
using Half_Life_3.Menu;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

/*
TODO:
 - Gravity Gun, Holy shit this is big
 - Story (Maybe do this before the previous one)
*/

namespace Half_Life_3
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    class Game1
    {
        public static PlayableCharacter Freeman { get; private set; }
        public static HelperCharacter Alyx { get; private set; }
        public static EntityManager EntityManager { get; private set; }
        public static DialogueManager DialogueManager { get; private set; }
        public static StoryManager StoryManager { get; private set; }

        public Game1()
        {
            /*
            // Artemis intro
            Video video = AssetLoader.Load<Video>(@"Resources\artemis", false);
            VideoPlayer player = new VideoPlayer();
            player.IsLooped = false;

            player.Play(video);
            */
            
            Freeman = new PlayableCharacter("Gordon Freeman", 0, 0);
            Alyx = new HelperCharacter("Alyx", 1500, 1500, true);
            DialogueManager = new DialogueManager();
            EntityManager = new EntityManager();
            StoryManager = new StoryManager();

            ArtemisEngine.RegisterMultiforms(new MenuMultiform("Main Menu"));
            ArtemisEngine.RegisterMultiforms(new GameMultiform("Game"));
            ArtemisEngine.RegisterMultiforms(new GUIMultiform("GUI"));

            ArtemisEngine.StartWith("Main Menu");
        }
    }
}
