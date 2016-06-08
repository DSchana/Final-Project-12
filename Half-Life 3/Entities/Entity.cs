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

        public Entity(string name) : base(name)
        {
            Type = EntityType.Entity;
            Rotation = 0;
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
    }
}
