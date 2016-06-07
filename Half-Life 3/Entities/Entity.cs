using Artemis.Engine.Forms;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Entities
{
    class Entity : PhysicalForm
    {
        public EntityType Type { get; internal set; }

        /// <summary>
        /// Hitbox for any in game entity
        /// </summary>
        public Rectangle BoundingBox { get; internal set; }

        /// <summary>
        /// Rotation value of entity
        /// </summary>
        public float Rotation { get; internal set; }

        public Entity(string name) : base(name)
        {
            
        }
    }
}
