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

        public Obstacle(string name, int x, int y, int width, int height) : base(name)
        {
            Type = EntityType.Obstacle;
            BoundingBox = new Rectangle(x, y, width, height);
            Rotation = 0;
        }

        public Obstacle(string name, Rectangle boundingBox) : base(name)
        {
            Type = EntityType.Obstacle;
            BoundingBox = boundingBox;
            Rotation = 0;
        }
    }
}
