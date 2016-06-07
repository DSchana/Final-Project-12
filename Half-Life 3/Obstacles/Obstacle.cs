using Artemis.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Obstacles
{
    class Obstacle : PhysicalForm
    {
        /// <summary>
        /// Collision rectangle of obstacle
        /// </summary>
        public Rectangle BoundingBox { get; private set; }

        /// <summary>
        /// The rotation of obstacle
        /// </summary>
        public float Rotation { get; private set; }

        public Obstacle(string name, int x, int y, int width, int height) : base(name)
        {
            BoundingBox = new Rectangle(x, y, width, height);
        }

        public Obstacle(string name, Rectangle boundingBox) : base(name)
        {
            BoundingBox = boundingBox;
        }
    }
}
