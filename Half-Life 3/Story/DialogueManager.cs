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
        private Color FontColour = new Color(156, 137, 72);
        private List<string> ToSay = new List<string>();
        private float SayRate = 0.004f;
        private float CurrentSay = 0.0f;

        public DialogueManager()
        {
            Font = AssetLoader.Load<SpriteFont>("hl_font", false);
            FontPosition = new Vector2(400, 900);
        }

        public void Write(Character sayer, string words)
        {
            sBuilder.Append(sayer.Name.ToUpper());
            sBuilder.Append(": " + words.ToUpper());

            ToSay.Add(sBuilder.ToString());

            sBuilder.Clear();
        }

        public void Write(string sayer, string words)
        {
            sBuilder.Append(sayer.ToUpper());
            sBuilder.Append(": " + words.ToUpper());

            ToSay.Add(sBuilder.ToString());

            sBuilder.Clear();
        }

        public void WriteNow(string sayer, string words)
        {
            sBuilder.Append(sayer.ToUpper());
            sBuilder.Append(": " + words.ToUpper());

            ArtemisEngine.RenderPipeline.RenderText(Font, sBuilder, FontPosition, FontColour);

            sBuilder.Clear();
        }

        public void Render()
        {
            if (ToSay.Count > 0)
            {
                if ((int)CurrentSay < 1)
                {
                    sBuilder.Append(ToSay[(int)CurrentSay]);
                    ArtemisEngine.RenderPipeline.RenderText(Font, sBuilder, FontPosition, FontColour);
                    sBuilder.Clear();
                    CurrentSay += SayRate;
                }
                else
                {
                    CurrentSay = 0.0f;
                    ToSay.RemoveAt(0);
                }
            }
        }
    }
}
