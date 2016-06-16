using Artemis.Engine;
using Half_Life_3.Entities.Weapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Entities.Characters
{
    class CombineSoldier : Character
    {
        /// <summary>
        /// Combine overwatch type
        /// </summary>
        public CombineType CType { get; private set; }

        public CombineSoldier(string name, CombineType type, int x, int y) : base(name, x, y)
        {
            Name = name;
            IsPlayable = false;
            CType = type;

            if (CType == CombineType.CivilProtection)
            {
                SetMaxHealth(40);
                CurrentWeapon = new Weapon("USP", this, WeaponType.USPMatch);
            }
            else if (CType == CombineType.OverwatchSoldier)
            {
                SetMaxHealth(50);
                CurrentWeapon = new Weapon("MP7", this, WeaponType.MP7);
            }
            else if (CType == CombineType.OverwatchElite)
            {
                SetMaxHealth(70);
                CurrentWeapon = new Weapon("MP7", this, WeaponType.MP7);
            }
            else if (CType == CombineType.Stalker)
            {
                SetMaxHealth(25);
                CurrentWeapon = new Weapon("USP", this, WeaponType.USPMatch);
            }

            CurrentWeapon.IsActive = true;

            Sprites = new Sprite();
            Sprites.ToggleAlwaysAnimate();
            
            Sprites.LoadDirectory(@"Content\Resources\Combine\MP7");
            Sprites.LoadDirectory(@"Content\Resources\Combine\USP");

            ChangeState("idle");

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

            WorldPosition = NewWorldPosition;
            ScreenPosition = WorldPosition - Game1.EntManager.CameraPosition;

            if (isMoving && !Sprites.CurrentState.Contains("move"))
            {
                ChangeState("move");
            }
            else if (!isMoving && !Sprites.CurrentState.Contains("idle"))
            {
                ChangeState("idle");
            }
        }

        /// <summary>
        /// Melee attack playable character if close enough.
        /// Shoot otherwise.
        /// </summary>
        private void Attack()
        {
            // TODO: Fire only if combine has unobstructed view of target
            if (CurrentWeapon.ClipAmmo <= 0)
            {
                ChangeState("reload", true);
                CurrentWeapon.Reload();
            }
            else if (Math.Sqrt(Math.Pow(Math.Abs(WorldPosition.X - Game1.Freeman.WorldPosition.X), 2) + Math.Pow(Math.Abs(WorldPosition.Y - Game1.Freeman.WorldPosition.Y), 2)) <= (int)CurrentWeapon.MeleeRange && !Sprites.CurrentState.Contains("meleeattack"))
            {
                ChangeState("meleeattack");
                CurrentWeapon.Fire(DamageType.Melee);
            }
            else if (!Sprites.CurrentState.Contains("shoot") && CurrentWeapon.ClipAmmo > 0)
            {
                ChangeState("shoot");
                CurrentWeapon.Fire();
            }
        }
    }
}
