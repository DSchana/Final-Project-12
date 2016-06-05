using System;
using System.Collections.Generic;
using Half_Life_3.Weapons;
using System.Linq;
using System.Text;

namespace Half_Life_3
{
    /// <summary>
    /// Stores all characters and determines how damage is dealt.
    /// Used to allow weapons to be hitscan rather than projectiles.
    /// </summary>
    class CharacterManager
    {
        /// <summary>
        /// List of Characters to handle
        /// </summary>
        public Dictionary<string, Character> Characters { get; private set; }

        public CharacterManager()
        {
            Characters = new Dictionary<string, Character>();
        }

        public void Add(Character character)
        {
            Characters.Add(character.Name, character);
        }

        public void Add(PlayableCharacter character)
        {
            Characters.Add(character.Name, character);
        }

        public void Kill(string name)
        {
            try
            {
                Characters.Remove(name);
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
            foreach (KeyValuePair<string, Character> character in Characters)
            {
                if (character.Value.CurrentWeapon.TypeDamage == DamageType.Hitscan)
                {
                    ScanHit(character.Value);
                }
                else if (character.Value.CurrentWeapon.TypeDamage == DamageType.Melee)
                {
                    MeeleHit(character.Value);
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
        public void DealDamage(Weapons.DamageType damageType)
        {
            foreach (KeyValuePair<string, Character> character in Characters)
            {
                if (damageType == Weapons.DamageType.Hitscan)
                {
                    ScanHit(character.Value);
                }
                else if (damageType == Weapons.DamageType.Melee)
                {
                    MeeleHit(character.Value);
                }
            }
        }

        /// <summary>
        /// Allow manager to determine damage type and perform appropriate actions
        /// </summary>
        /// <param name="character">The character wielding the weapon</param>
        /// <param name="damageType">The type of damage a character's weapon will be forced to exert</param>
        public void DealDamage(Character character, DamageType damageType)
        {
            if (damageType == DamageType.Hitscan)
            {
                ScanHit(character);
            }
            else if (damageType == DamageType.Melee)
            {
                MeeleHit(character);
            }
        }

        /// <summary>
        /// Allow manager to deal damage from hitscan weapons
        /// </summary>
        public void ScanHit(Character character)
        {
            float slope = (float)(Math.Sin(character.Rotation) / Math.Cos(character.Rotation));
            float y_int = character.WorldPosition.Y - (slope * character.WorldPosition.X);
            Character actualTarget = null;
            double actualDistanceToTarget = double.MaxValue;

            // Check collision and find target
            foreach (KeyValuePair<string, Character> potentialTarget in Characters)
            {
                if (Characters[character.Name] != potentialTarget.Value)
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

            if (actualTarget != null)
            {
                if (actualDistanceToTarget <= character.CurrentWeapon.Range)
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
            List<Character> actualTargets = new List<Character>();

            foreach (KeyValuePair<string, Character> potentialTarget in Characters)
            {
                if (Characters[character.Name] != potentialTarget.Value)
                {
                    continue;
                }

                // Case 1: Line collides with left side of BoundingBox
                // case 2: Line collides with left side of BoundingBox. Used if case 1 is false
                if ((slope * potentialTarget.Value.BoundingBox.X) + y_int - potentialTarget.Value.BoundingBox.Y <= potentialTarget.Value.BoundingBox.Height ||
                    (slope * (potentialTarget.Value.BoundingBox.X + potentialTarget.Value.BoundingBox.Width)) + y_int - potentialTarget.Value.BoundingBox.Y <= potentialTarget.Value.BoundingBox.Height)
                {
                    double potentialDistanceToTarget = Math.Sqrt(Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.X - character.WorldPosition.X), 2) + Math.Pow(Math.Abs(potentialTarget.Value.WorldPosition.Y - character.WorldPosition.Y), 2));

                    if (potentialDistanceToTarget < character.CurrentWeapon.MeleeRange)
                    {
                        actualTargets.Add(potentialTarget.Value);
                    }
                }
            }

            foreach (var target in actualTargets)
            {
                target.TakeDamage(character.CurrentWeapon.MeleeDamage);
            }
        }
    }
}
