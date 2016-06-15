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
        /// <summary>
        /// Type of entity
        /// </summary>
        public EntityType Type { get; internal set; }

        /// <summary>
        /// Hitbox for any in game entity
        /// </summary>
        public Rectangle BoundingBox { get; internal set; }

        /// <summary>
        /// Entity position relative to the world
        /// </summary>
        public new Vector2 WorldPosition { get; internal set; }

        /// <summary>
        /// Entity position relative to the screen
        /// </summary>
        public new Vector2 ScreenPosition { get; internal set; }

        /// <summary>
        /// True if entity cannot be destroyed (For example, Weapons)
        /// </summary>
        public bool Indestructible { get; internal set; }

        /// <summary>
        /// Damage an entity can sustain before being eliminated.
        /// Health does not decrease if entity is Indestructible
        /// </summary>
        public float Health { get; internal set; } = 1;

        /// <summary>
        /// Rotation value of entity
        /// </summary>
        public float Rotation { get; internal set; }

        /// <summary>
        /// Sprite models of this entity
        /// </summary>
        public Sprite Sprites { get; internal set; }

        public Entity(string name, int x, int y) : base(name)
        {
            Type = EntityType.Entity;
            Rotation = 0;

            SetLocation(x, y);

            AddUpdater(UpdateBoundingBox);
        }

        public void UpdateBoundingBox()
        {
            BoundingBox = new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, BoundingBox.Width, BoundingBox.Height);
        }

        public void SetLocation(Vector2 newPos)
        {
            WorldPosition = newPos;
        }

        public void SetLocation(int x, int y)
        {
            WorldPosition = new Vector2(x, y);
        }

        public void TakeDamage(int damage)
        {
            if (!Indestructible)
                Health -= damage;

            if (Health <= 0)
            {
                Health = 0;
                Kill();
            }
        }

        public void Show()
        {
            Sprites.Render(ScreenPosition, Rotation);
        }
    }
}
