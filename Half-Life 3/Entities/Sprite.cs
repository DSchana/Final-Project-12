using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Entities
{
    class Sprite
    {
        /// <summary>
        /// Holds all Textures to render
        /// </summary>
        public Dictionary<string, List<Texture2D>> Textures { get; private set; }

        /// <summary>
        /// Animation state
        /// </summary>
        public string CurrentState { get; private set; }

        public Sprite()
        {
            Textures = new Dictionary<string, List<Texture2D>>();
        }

        // SO FREAKING NOT DONE. GOD YOU HAVE SO MUCH SHIT TO DO
    }
}
