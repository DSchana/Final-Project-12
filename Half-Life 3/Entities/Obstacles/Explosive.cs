using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Half_Life_3.Entities.Obstacles
{
    class Explosive : Obstacle
    {
        /// <summary>
        /// True if explosive sustains any damage
        /// </summary>
        public bool IsBurning { get; private set; }

        public Explosive(string name, int x, int y, int width, int height) : base(name, x, y, width, height, false)
        {
            Type = EntityType.Explosive;
            IsInteractable = true;
            IsBurning = false;
            Health = 10;

            AddUpdater(UpdateHealth);
            AddUpdater(Explode);
        }

        public Explosive(string name, Rectangle boundingBox) : base(name, boundingBox, false)
        {
            Type = EntityType.Explosive;
            IsInteractable = true;
            IsBurning = false;
            Health = 10;
        }

        public void UpdateHealth()
        {
            // Animate FIRE
            if (IsBurning)
            {
                Health -= (float)0.3;
            }
        }

        public new void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 10)
            {
                IsBurning = true;
            }
        }

        private void Explode()
        {
            if (Health <= 0)
            {
                // Animate explosions
                Game1.EntManager.DealDamage(this);
            }
        }
    }
}
