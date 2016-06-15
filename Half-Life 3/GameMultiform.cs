﻿using Artemis.Engine;
using Artemis.Engine.Multiforms;
using Artemis.Engine.Graphics;
using Artemis.Engine.Assets;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3
{
    class GameMultiform : Multiform
    {
        public Texture2D Crosshairs { get; private set; }

        public GameMultiform() : base() { }
        public GameMultiform(string name) : base(name) { }

        public override void Construct(MultiformConstructionArgs args)
        {
            Console.WriteLine("LET THE GAMES BEGIN");

            Crosshairs = AssetLoader.Load<Texture2D>(@"Resources\Crosshairs", false);

            AddUpdater(MainUpdater);
            AddRenderer(MainRenderer);
        }

        public void MainUpdater()
        {
            // Move the map around here
            // Probably manage the story or call another thing to do that
            Game1.EntManager.Update();
        }

        public void MainRenderer()
        {
            Game1.EntManager.Render();
            ArtemisEngine.RenderPipeline.Render(Crosshairs, ArtemisEngine.Mouse.PositionVector, null, null, 0, PositionOffsets.Center, originIsRelative: true);
        }
    }
}
