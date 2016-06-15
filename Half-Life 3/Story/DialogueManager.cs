using Artemis.Engine;
using Artemis.Engine.Assets;
using Half_Life_3.Entities.Characters;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Story
{
    class DialogueManager
    {
        /// <summary>
        /// Font used for Dialogue
        /// </summary>
        public SpriteFont Font { get; private set; }

        /// <summary>
        /// Position on the screen the text will be rendered
        /// </summary>
        public Vector2 FontPosition { get; private set; }

        private StringBuilder sBuilder = new StringBuilder();

        public DialogueManager()
        {
            Font = AssetLoader.Load<SpriteFont>("hl_font", false);
            FontPosition = new Vector2(100, 1000);
        }

        public void Write(Character sayer, string words)
        {
            sBuilder.Append(sayer.Name.ToUpper());
            sBuilder.Append(": " + words);

            ArtemisEngine.RenderPipeline.RenderText(Font, sBuilder, FontPosition, new Color(247, 209, 37));

            sBuilder.Clear();
        }
    }
}
