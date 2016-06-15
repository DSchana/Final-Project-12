using Artemis.Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Entities.Obstacles
{
    class Obstacle : Entity
    {
        /// <summary>
        /// True if characters can interact with this obstacle
        /// </summary>
        public bool IsInteractable { get; internal set; }

        public Obstacle(string name, int x, int y, int width, int height, bool indestructible = true) : base(name, x, y)
        {
            Type = EntityType.Obstacle;
            Indestructible = indestructible;
            BoundingBox = new Rectangle(x, y, width, height);
        }

        public Obstacle(string name, Rectangle boundingBox, bool indestructible = true) : base(name, boundingBox.X, boundingBox.Y)
        {
            Type = EntityType.Obstacle;
            Indestructible = indestructible;
            BoundingBox = boundingBox;
        }
    }
}
