using Artemis.Engine;
using Artemis.Engine.Assets;
using Artemis.Engine.Multiforms;
using Half_Life_3.Entities.Obstacles;
using Half_Life_3.Entities.Characters;
using Half_Life_3.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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

        /// <summary>
        /// Backgroud music
        /// </summary>
        public Song BackgroundMusic { get; private set; }

        public GameMultiform() : base() { }
        public GameMultiform(string name) : base(name) { }

        private bool NewSceneLoaded = false;
        private float KillTimer = 0.0f;

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
            if (Game1.StoryManager.Flags["helicopter"].IsActive)
            {
                if (Game1.Jim.Health <= 0)
                {
                    Environment.ExitCode = 0;
                    Environment.Exit(Environment.ExitCode);

                    Game1.EntityManager.KillAll();
                    ArtemisEngine.MultiformManager.Activate(this, "Main Menu");
                    ArtemisEngine.MultiformManager.Deactivate("GUI");
                    Deactivate();
                }
            }
            else
            {
                if (Game1.Freeman.Health <= 0)
                {
                    Environment.ExitCode = 0;
                    Environment.Exit(Environment.ExitCode);

                    Game1.EntityManager.KillAll();
                    ArtemisEngine.MultiformManager.Activate(this, "Main Menu");
                    ArtemisEngine.MultiformManager.Deactivate("GUI");
                    Deactivate();
                }
            }

            BackgroundPosition = -Game1.EntityManager.CameraPosition;
            Game1.EntityManager.Update();
            Game1.StoryManager.TriggerFlag();

            if (!NewSceneLoaded && Game1.StoryManager.Flags["helicopter"].IsActive)
            {
                Background = AssetLoader.Load<Texture2D>(@"Resources\Backgrounds\Lake", false);
                Game1.EntityManager.KillAll();
                Game1.EntityManager.Add(Game1.Jim);
                AddUpdater(Game1.Jim.Rotate);
                AddUpdater(Game1.Jim.Move);
                NewSceneLoaded = true;
            }
        }

        public void MainRenderer()
        {
            // new Rectangle((int)Game1.EntManager.CameraPosition.X, (int)Game1.EntManager.CameraPosition.Y, ArtemisEngine.DisplayManager.WindowResolution.Width, ArtemisEngine.DisplayManager.WindowResolution.Height)
            Vector2 BackgroundCrop = Game1.EntityManager.CameraPosition;

            if (BackgroundCrop.X < 0)
            {
                BackgroundCrop.X = 0.0f;
            }
            if (BackgroundCrop.X + ArtemisEngine.DisplayManager.WindowResolution.Width > Background.Width)
            {
                BackgroundCrop.X = Background.Width - ArtemisEngine.DisplayManager.WindowResolution.Width;
            }
            if (BackgroundCrop.Y < 0)
            {
                BackgroundCrop.Y = 0.0f;
            }
            if (BackgroundCrop.Y + ArtemisEngine.DisplayManager.WindowResolution.Height > Background.Height)
            {
                BackgroundCrop.Y = Background.Height - ArtemisEngine.DisplayManager.WindowResolution.Height;
            }

            ArtemisEngine.RenderPipeline.Render(Background, BackgroundPosition, /*new Rectangle((int)BackgroundCrop.X, (int)BackgroundCrop.Y, ArtemisEngine.DisplayManager.WindowResolution.Width, ArtemisEngine.DisplayManager.WindowResolution.Height)*/null, null, 0, null, Vector2.One * 2);
            Game1.EntityManager.Render();
            Game1.DialogueManager.Render();

            if (Game1.StoryManager.Flags["helicopter"].IsActive)
            {
                if (Game1.Jim.WorldPosition.X < 0 ||
                    Game1.Jim.WorldPosition.X > Background.Width ||
                    Game1.Jim.WorldPosition.Y < 0||
                    Game1.Jim.WorldPosition.X > Background.Height)
                {
                    //Game1.DialogueManager.WriteNow("Hazard Suit Vocal Aid Module", "This area is not available. Death imminent");
                    KillTimer += 0.03f;
                    if (KillTimer == 5.0f)
                    {
                        //Game1.Jim.TakeDamage(500);
                    }
                }
                else
                {
                    KillTimer = 0.0f;
                }
            }
            else
            {
                if (Game1.Freeman.WorldPosition.X < 0 ||
                    Game1.Freeman.WorldPosition.X > Background.Width ||
                    Game1.Freeman.WorldPosition.Y < 0 ||
                    Game1.Freeman.WorldPosition.X > Background.Height)
                {
                    //Game1.DialogueManager.WriteNow("Hazard Suit Vocal Aid Module", "This area is not available. Death imminent");
                    KillTimer += 0.03f;
                    if (KillTimer >= 15.0f)
                    {
                        //Game1.Freeman.TakeDamage(100);
                    }
                }
                else
                {
                    KillTimer = 0.0f;
                }
            }
        }
    }
}
