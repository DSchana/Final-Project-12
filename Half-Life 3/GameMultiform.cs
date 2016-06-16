using Artemis.Engine;
using Artemis.Engine.Assets;
using Artemis.Engine.Multiforms;
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
            Background = AssetLoader.Load<Texture2D>(@"Resources\Backgrounds\Graveyard", false);

            Game1.EntManager.Add(Game1.Freeman);
            for (int i = 0; i < 10; i++)
                Game1.EntManager.Add(new CombineSoldier("Combine" + i, CombineType.CivilProtection, 1000, 1000));

            AddUpdater(MainUpdater);
            AddRenderer(MainRenderer);
        }

        public void MainUpdater()
        {
            BackgroundPosition = -Game1.EntManager.CameraPosition;
            // Probably manage the story or call another thing to do that
            Game1.EntManager.Update();
        }

        public void MainRenderer()
        {
            ArtemisEngine.RenderPipeline.Render(Background, BackgroundPosition, null, null, 0, null, Vector2.One * 6);
            Game1.EntManager.Render();
        }
    }
}
