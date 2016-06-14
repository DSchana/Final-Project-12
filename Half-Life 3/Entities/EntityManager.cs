using System;
using System.Collections.Generic;
using Half_Life_3.Entities.Weapons;
using Half_Life_3.Entities.Characters;
using Half_Life_3.Entities.Obstacles;
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
                if (entity.Value.Type == EntityType.Character)
                {
                    Character character = entity.Value as Character;
                    if (character.CurrentWeapon.TypeDamage == DamageType.Hitscan)
                    {
                        ScanHit(character);
                    }
                    else if (character.CurrentWeapon.TypeDamage == DamageType.Melee)
                    {
                        MeeleHit(character);
                    }
                }
                else if (entity.Value.Type == EntityType.Explosive)
                {
                    Explosive explosive = entity.Value as Explosive;
                    ExplosiveDamage(explosive);
                }
            }
        }

        /// <summary>
        /// Allow manager to determine damage type and perform appropriate actions
        /// </summary>
        /// <param name="character">The character wielding the weapon</param>
        public void DealDamage(Entity entity)
        {
            if (entity.Type == EntityType.Character)
            {
                Character character = entity as Character;
                if (character.CurrentWeapon.TypeDamage == DamageType.Hitscan)
                {
                    ScanHit(character);
                }
                else if (character.CurrentWeapon.TypeDamage == DamageType.Melee)
                {
                    MeeleHit(character);
                }
            }
            else if (entity.Type == EntityType.Explosive)
            {
                Explosive explosive = entity as Explosive;
                ExplosiveDamage(explosive);
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
                if (entity.Value.Type == EntityType.Character)
                {
                    Character character = entity.Value as Character;
                    if (damageType == DamageType.Hitscan)
                    {
                        ScanHit(character);
                    }
                    else if (damageType == DamageType.Melee)
                    {
                        MeeleHit(character);
                    }
                }
                else if (entity.Value.Type == EntityType.Explosive && damageType == DamageType.Projectile)
                {
                    Explosive explosive = entity.Value as Explosive;
                    ExplosiveDamage(explosive);
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
            if (entity.Type == EntityType.Character)
            {
                Character character = entity as Character;
                if (damageType == DamageType.Hitscan)
                {
                    ScanHit(character);
                }
                else if (damageType == DamageType.Melee)
                {
                    MeeleHit(character);
                }
            }
            else if (entity.Type == EntityType.Explosive && damageType == DamageType.Projectile)
            {
                Explosive explosive = entity as Explosive;
                ExplosiveDamage(explosive);
            }
        }

        /// <summary>
        /// Allow manager to deal damage from hitscan weapons
        /// </summary>
        public void ScanHit(Character character)
        {
            float slope = (float)(Math.Sin(character.Rotation) / Math.Cos(character.Rotation));
            float y_int = character.WorldPosition.Y - (slope * character.WorldPosition.X);
            Entity actualTarget = null;
            double actualDistanceToTarget = double.MaxValue;

            // Check collision and find target
            foreach (KeyValuePair<string, Entity> potentialTarget in Entities)
            {
                if (Entities[character.Name] != potentialTarget.Value)
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
                        actualDistanceToTarget = Math.Sqrt(Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.X - character.WorldPosition.X), 2) + Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.Y - character.WorldPosition.Y), 2));
                        continue;
                    }

                    // Find closet target. This is so if there are many targets in a line, only the closest is hit
                    double potentialDistanceToTarget = Math.Sqrt(Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.X - character.WorldPosition.X), 2) + Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.Y - character.WorldPosition.Y), 2));

                    if (potentialDistanceToTarget < actualDistanceToTarget)
                    {
                        actualTarget = potentialTarget.Value;
                        actualDistanceToTarget = potentialDistanceToTarget;
                    }
                }
            }

            // true if target is in front of character and not behind
            if (actualTarget != null && Math.Abs(character.WorldPosition.X + Math.Cos(character.Rotation) - actualTarget.WorldPosition.X) < Math.Abs(character.WorldPosition.X - actualTarget.WorldPosition.X))
            {
                if (actualDistanceToTarget <= (int)character.CurrentWeapon.Range)
                {
                    actualTarget.TakeDamage(character.CurrentWeapon.RangeDamage);
                }
                else
                {
                    actualTarget.TakeDamage(character.CurrentWeapon.RangeDamage / 2);  // Damage drop-off if target is too far
                }
            }
        }

        public void MeeleHit(Character character)
        {
            float slope = (float)(Math.Sin(character.Rotation) / Math.Cos(character.Rotation));
            float y_int = character.WorldPosition.Y - (slope * character.WorldPosition.X);
            List<Entity> actualTargets = new List<Entity>();

            foreach (KeyValuePair<string, Entity> potentialTarget in Entities)
            {
                if (Entities[character.Name] != potentialTarget.Value)
                {
                    continue;
                }

                // Case 1: Line collides with left side of BoundingBox
                // case 2: Line collides with left side of BoundingBox. Used if case 1 is false
                if ((slope * potentialTarget.Value.BoundingBox.X) + y_int - potentialTarget.Value.BoundingBox.Y <= potentialTarget.Value.BoundingBox.Height ||
                    (slope * (potentialTarget.Value.BoundingBox.X + potentialTarget.Value.BoundingBox.Width)) + y_int - potentialTarget.Value.BoundingBox.Y <= potentialTarget.Value.BoundingBox.Height)
                {
                    double potentialDistanceToTarget = Math.Sqrt(Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.X - character.WorldPosition.X), 2) + Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.Y - character.WorldPosition.Y), 2));

                    if (potentialDistanceToTarget < (int)character.CurrentWeapon.MeleeRange)
                    {
                        actualTargets.Add(potentialTarget.Value);
                    }
                }
            }

            foreach (var target in actualTargets)
            {
                // true if target is in front of character and not behind
                if (Math.Abs(character.WorldPosition.X + Math.Cos(character.Rotation) - target.WorldPosition.X) < Math.Abs(character.WorldPosition.X - target.WorldPosition.X))
                {
                    target.TakeDamage(character.CurrentWeapon.MeleeDamage);
                }
            }
        }
        
        public void ExplosiveDamage(Explosive explosive)
        {
            foreach (KeyValuePair<string, Entity> entity in Entities)
            {
                if (entity.Value == explosive)
                {
                    continue;
                }

                double distance = Math.Sqrt(Math.Pow(Math.Abs(entity.Value.WorldPosition.X - explosive.WorldPosition.X), 2) + Math.Pow(Math.Abs(entity.Value.WorldPosition.Y - explosive.WorldPosition.Y), 2));

                if (distance <= 50)
                {
                    entity.Value.TakeDamage(explosive.ExplosiveDamage);
                }
                else if (distance <= 100)
                {
                    entity.Value.TakeDamage(explosive.ExplosiveDamage / 2);
                }
                else if (distance <= 400)
                {
                    entity.Value.TakeDamage(explosive.ExplosiveDamage / 4);
                }
                else if (distance <= 800)
                {
                    entity.Value.TakeDamage(explosive.ExplosiveDamage / 8);
                }
            }
        }

        public void Update()
        {
            foreach (var entity in Entities.Values)
            {
                entity.Sprites.Update();
            }
        }

        public void Render()
        {
            foreach (var entity in Entities.Values)
            {
                entity.Show();
            }

            Game1.Freeman.Say("TEST DIALOGUE. HERE COME THE COMBINE");
        }
    }
}
