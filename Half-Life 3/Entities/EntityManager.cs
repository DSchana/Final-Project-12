using System;
using System.Collections.Generic;
using Half_Life_3.Entities.Weapons;
using Half_Life_3.Entities.Characters;
using System.Linq;
using System.Text;

namespace Half_Life_3.Entities
{
    /// <summary>
    /// Stores all characters and determines how damage is dealt.
    /// Used to allow weapons to be hitscan rather than projectiles.
    /// </summary>
    class EntityManager
    {
        /// <summary>
        /// List of Characters to handle
        /// </summary>
        public Dictionary<string, Entity> Entities { get; private set; }

        public EntityManager()
        {
            Entities = new Dictionary<string, Entity>();
        }

        public void Add(Entity entity)
        {
            Entities.Add(entity.Name, entity);
        }

        // Overload Add method for other classes

        public void Kill(string name)
        {
            try
            {
                Entities.Remove(name);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(String.Format("'{0}' not a character. \nError at: \n'{1}'", name, e.Source));
            }
        }

        /// <summary>
        /// Allow manager to determine damage type and perform appropriate actions
        /// </summary>
        public void DealDamage()
        {
            foreach (KeyValuePair<string, Entity> entity in Entities)
            {
                if (entity.Value.CurrentWeapon.TypeDamage == DamageType.Hitscan)
                {
                    ScanHit(entity.Value);
                }
                else if (entity.Value.CurrentWeapon.TypeDamage == DamageType.Melee)
                {
                    MeeleHit(entity.Value);
                }
            }
        }

        /// <summary>
        /// Allow manager to determine damage type and perform appropriate actions
        /// </summary>
        /// <param name="character">The character wielding the weapon</param>
        public void DealDamage(Character character)
        {
            if (character.CurrentWeapon.TypeDamage == DamageType.Hitscan)
            {
                ScanHit(character);
            }
            else if (character.CurrentWeapon.TypeDamage == DamageType.Melee)
            {
                MeeleHit(character);
            }
        }

        /// <summary>
        /// Allow manager to determine damage type and perform appropriate actions
        /// </summary>
        /// <param name="damageType">The type of damage a character's weapon will be forced to exert</param>
        public void DealDamage(DamageType damageType)
        {
            foreach (KeyValuePair<string, Entity> entity in Entities)
            {
                if (damageType == DamageType.Hitscan)
                {
                    ScanHit(entity.Value);
                }
                else if (damageType == DamageType.Melee)
                {
                    MeeleHit(entity.Value);
                }
            }
        }

        /// <summary>
        /// Allow manager to determine damage type and perform appropriate actions
        /// </summary>
        /// <param name="character">The character wielding the weapon</param>
        /// <param name="damageType">The type of damage a character's weapon will be forced to exert</param>
        public void DealDamage(Entity entity, DamageType damageType)
        {
            if (damageType == DamageType.Hitscan)
            {
                ScanHit(entity);
            }
            else if (damageType == DamageType.Melee)
            {
                MeeleHit(entity);
            }
        }

        /// <summary>
        /// Allow manager to deal damage from hitscan weapons
        /// </summary>
        public void ScanHit(Entity entity)
        {
            float slope = (float)(Math.Sin(entity.Rotation) / Math.Cos(entity.Rotation));
            float y_int = entity.WorldPosition.Y - (slope * entity.WorldPosition.X);
            Entity actualTarget = null;
            double actualDistanceToTarget = double.MaxValue;

            // Check collision and find target
            foreach (KeyValuePair<string, Entity> potentialTarget in Entities)
            {
                if (Entities[entity.Name] != potentialTarget.Value)
                {
                    continue;
                }

                // Case 1: Line collides with left side of BoundingBox
                // case 2: Line collides with left side of BoundingBox. Used if case 1 is false
                if ((slope * potentialTarget.Value.BoundingBox.X) + y_int - potentialTarget.Value.BoundingBox.Y <= potentialTarget.Value.BoundingBox.Height ||
                    (slope * (potentialTarget.Value.BoundingBox.X + potentialTarget.Value.BoundingBox.Width)) + y_int - potentialTarget.Value.BoundingBox.Y <= potentialTarget.Value.BoundingBox.Height)
                {
                    if (actualTarget == null)
                    {
                        actualTarget = potentialTarget.Value;
                        actualDistanceToTarget = Math.Sqrt(Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.X - entity.WorldPosition.X), 2) + Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.Y - entity.WorldPosition.Y), 2));
                        continue;
                    }

                    // Find closet target. This is so if there are many targets in a line, only the closest is hit
                    double potentialDistanceToTarget = Math.Sqrt(Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.X - entity.WorldPosition.X), 2) + Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.Y - entity.WorldPosition.Y), 2));

                    if (potentialDistanceToTarget < actualDistanceToTarget)
                    {
                        actualTarget = potentialTarget.Value;
                        actualDistanceToTarget = potentialDistanceToTarget;
                    }
                }
            }

            // true if target is in front of character and not behind
            if (actualTarget != null && Math.Abs(entity.WorldPosition.X + Math.Cos(entity.Rotation) - actualTarget.WorldPosition.X) < Math.Abs(entity.WorldPosition.X - actualTarget.WorldPosition.X))
            {
                if (actualDistanceToTarget <= (int)entity.CurrentWeapon.Range)
                {
                    actualTarget.TakeDamage(entity.CurrentWeapon.RangeDamage);
                }
                else
                {
                    actualTarget.TakeDamage(entity.CurrentWeapon.RangeDamage / 2);  // Damage drop-off if target is too far
                }
            }
        }

        public void MeeleHit(Entity entity)
        {
            float slope = (float)(Math.Sin(entity.Rotation) / Math.Cos(entity.Rotation));
            float y_int = entity.WorldPosition.Y - (slope * entity.WorldPosition.X);
            List<Entity> actualTargets = new List<Entity>();

            foreach (KeyValuePair<string, Entity> potentialTarget in Entities)
            {
                if (Entities[entity.Name] != potentialTarget.Value)
                {
                    continue;
                }

                // Case 1: Line collides with left side of BoundingBox
                // case 2: Line collides with left side of BoundingBox. Used if case 1 is false
                if ((slope * potentialTarget.Value.BoundingBox.X) + y_int - potentialTarget.Value.BoundingBox.Y <= potentialTarget.Value.BoundingBox.Height ||
                    (slope * (potentialTarget.Value.BoundingBox.X + potentialTarget.Value.BoundingBox.Width)) + y_int - potentialTarget.Value.BoundingBox.Y <= potentialTarget.Value.BoundingBox.Height)
                {
                    double potentialDistanceToTarget = Math.Sqrt(Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.X - entity.WorldPosition.X), 2) + Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.Y - entity.WorldPosition.Y), 2));

                    if (potentialDistanceToTarget < (int)entity.CurrentWeapon.MeleeRange)
                    {
                        actualTargets.Add(potentialTarget.Value);
                    }
                }
            }

            foreach (var target in actualTargets)
            {
                // true if target is in front of character and not behind
                if (Math.Abs(entity.WorldPosition.X + Math.Cos(entity.Rotation) - target.WorldPosition.X) < Math.Abs(entity.WorldPosition.X - target.WorldPosition.X))
                {
                    target.TakeDamage(entity.CurrentWeapon.MeleeDamage);
                }
            }
        }
    }
}
