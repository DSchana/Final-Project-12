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
        public static EntityManager EntManager { get; private set; }
        public static DialogueManager DiagManager { get; private set; }

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
            EntManager = new EntityManager();
            DiagManager = new DialogueManager();

            EntManager.Add(Freeman);
            EntManager.Add(new CombineSoldier("Combine", CombineType.CivilProtection, 1000, 1000));

            ArtemisEngine.RegisterMultiforms(new MenuMultiform("Main Menu"));
            ArtemisEngine.RegisterMultiforms(new GameMultiform("Game"));

            ArtemisEngine.StartWith("Main Menu");
        }
    }
}
