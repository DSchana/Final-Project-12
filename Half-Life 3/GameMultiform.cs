using Artemis.Engine;
using Artemis.Engine.Assets;
using Artemis.Engine.Multiforms;
using Half_Life_3.Entities.Obstacles;
using Half_Life_3.Entities.Characters;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3
{
    class GameMultiform : Multiform
    {
        /// <summary>
        /// Position of worldrelative to screen
        /// </summary>
        public Vector2 BackgroundPosition { get; private set; }

        /// <summary>
        /// Image of background
        /// </summary>
        public Texture2D Background { get; private set; }

        public GameMultiform() : base() { }
        public GameMultiform(string name) : base(name) { }

        public override void Construct(MultiformConstructionArgs args)
        {
            Console.WriteLine("LET THE GAMES BEGIN");
            Background = AssetLoader.Load<Texture2D>(@"Resources\Backgrounds\Graveyard.png", false);

            Game1.EntityManager.Add(Game1.Freeman);
            Game1.EntityManager.Add(Game1.Alyx);
            Game1.EntityManager.Add(new Obstacle("Eli Grave", 500, 230, 162, 261));

            AddUpdater(MainUpdater);
            AddRenderer(MainRenderer);
        }

        public void MainUpdater()
        {
            BackgroundPosition = -Game1.EntityManager.CameraPosition;
            Game1.EntityManager.Update();
            Game1.StoryManager.TriggerFlag();
        }

        public void MainRenderer()
        {
            // new Rectangle((int)Game1.EntManager.CameraPosition.X, (int)Game1.EntManager.CameraPosition.Y, ArtemisEngine.DisplayManager.WindowResolution.Width, ArtemisEngine.DisplayManager.WindowResolution.Height)
            ArtemisEngine.RenderPipeline.Render(Background, BackgroundPosition, null, null, 0, null);  // Fix this to only render part of the background that is shown
            Game1.EntityManager.Render();
            Game1.DialogueManager.Render();
        }
    }
}
