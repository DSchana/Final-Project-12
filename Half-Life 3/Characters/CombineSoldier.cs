using Artemis.Engine.Graphics.Animation;
using Half_Life_3.Weapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Characters
{
    class Combine : Character
    {
        /// <summary>
        /// Combine overwatch type
        /// </summary>
        public CombineType Type { get; private set; }

        public Combine(string name, CombineType type, string AnimationFile) : base(name)
        {
            Name = name;
            IsPlayable = false;
            Type = type;
            AAMLReader = new AAMLFileReader(AnimationFile);

            if (Type == CombineType.CivilProtection)
            {
                SetMaxHealth(40);
                CurrentWeapon = new Weapon(this, WeaponType.USPMatch);
            }
            else if (Type == CombineType.OverwatchSoldier)
            {
                SetMaxHealth(50);
                CurrentWeapon = new Weapon(this, WeaponType.MP7);
            }
            else if (Type == CombineType.OverwatchElite)
            {
                SetMaxHealth(70);
                CurrentWeapon = new Weapon(this, WeaponType.MP7);
            }
            else if (Type == CombineType.Stalker)
            {
                SetMaxHealth(25);
                CurrentWeapon = new Weapon(this, WeaponType.USPMatch);
            }

            AddUpdater(Rotate);
            AddUpdater(Move);
            AddUpdater(Attack);
        }

        /// <summary>
        /// Rotate combine to always face the playable character
        /// </summary>
        private void Rotate()
        {
            Vector2 direction = Game1.Freeman.WorldPosition - WorldPosition;
            direction.Normalize();

            Rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        /// <summary>
        /// Move combine into proper firing range of playable character
        /// </summary>
        private void Move()
        {
            bool isMoving = false;
            Vector2 NewWorldPosition = new Vector2(WorldPosition.X, WorldPosition.Y);

            if (Math.Sqrt(Math.Pow(Math.Abs(WorldPosition.X - Game1.Freeman.WorldPosition.X), 2) + Math.Pow(Math.Abs(WorldPosition.Y - Game1.Freeman.WorldPosition.Y), 2)) > (int)CurrentWeapon.Range)
            {
                isMoving = true;
                NewWorldPosition.X += (float)(Speed * Math.Cos(Rotation));
                NewWorldPosition.Y += (float)(Speed * Math.Sin(Rotation));
            }

            if (isMoving)
            {
                ChangeState("Move");
            }
            else
            {
                ChangeState("Idle");
            }
        }

        /// <summary>
        /// Melee attack playable character if close enough.
        /// Shoot otherwise.
        /// </summary>
        private void Attack()
        {
            ChangeState("Attack");
            // TODO: Fire only if combine has unobstructed view of target
            if (Math.Sqrt(Math.Pow(Math.Abs(WorldPosition.X - Game1.Freeman.WorldPosition.X), 2) + Math.Pow(Math.Abs(WorldPosition.Y - Game1.Freeman.WorldPosition.Y), 2)) <= (int)CurrentWeapon.MeleeRange)
            {
                CurrentWeapon.Fire(DamageType.Melee);
            }
            else
            {
                CurrentWeapon.Fire();
            }
        }
    }
}
