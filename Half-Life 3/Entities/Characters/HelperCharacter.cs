using Artemis.Engine;
using Half_Life_3.Entities.Weapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// TODO: Fix the AI

namespace Half_Life_3.Entities.Characters
{
    class HelperCharacter : Character
    {
        private float AttackDelay;
        private float CurrentAttackTime = 0.0f;

        public HelperCharacter(string name, int x, int y, bool indestructible = false) : base(name, x, y)
        {
            Name = name;
            IsPlayable = false;

            Indestructible = indestructible;

            SetMaxHealth(40);
            CurrentWeapon = new Weapon("USPMatch", this, WeaponType.USPMatch);
            AttackDelay = 0.07f;

            CurrentWeapon.IsActive = true;

            Sprites = new Sprite();
            Sprites.ToggleAlwaysAnimate();
            
            Sprites.LoadDirectory(@"Content\Resources\Gordon Freeman\USPMatch");

            ChangeState("idle");

            AddUpdater(UpdaterCheck);
            AddUpdater(Rotate);
            AddUpdater(Move);
            AddUpdater(Attack);
        }

        private void UpdaterCheck()
        {
            if (Health <= 0)
            {
                RemoveUpdater(Rotate);
                RemoveUpdater(Move);
                RemoveUpdater(Attack);

                RemoveUpdater(UpdaterCheck);
            }
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

            int collisionsValue = Game1.EntityManager.IsCollisionFree(this, new Rectangle((int)NewWorldPosition.X, (int)NewWorldPosition.Y, BoundingBox.Width, BoundingBox.Height));
            if (collisionsValue == 0)
            {
                WorldPosition = NewWorldPosition;
            }
            else if (collisionsValue == 1)
            {
                WorldPosition = new Vector2(NewWorldPosition.X, WorldPosition.Y);
            }
            else if (collisionsValue == 2)
            {
                WorldPosition = new Vector2(WorldPosition.X, NewWorldPosition.Y);
            }

            ScreenPosition = WorldPosition - Game1.EntityManager.CameraPosition;

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
            CurrentAttackTime += AttackDelay;

            if (CurrentWeapon.ClipAmmo <= 0)
            {
                ChangeState("reload", true);
                CurrentWeapon.Reload();
            }
            else if (Math.Sqrt(Math.Pow(Math.Abs(WorldPosition.X - Game1.Freeman.WorldPosition.X), 2) + Math.Pow(Math.Abs(WorldPosition.Y - Game1.Freeman.WorldPosition.Y), 2)) <= (int)CurrentWeapon.MeleeRange && !Sprites.CurrentState.Contains("meleeattack"))
            {
                ChangeState("meleeattack");
                // CurrentWeapon.Fire(DamageType.Melee);
            }
            else if (!Sprites.CurrentState.Contains("shoot") && CurrentWeapon.ClipAmmo > 0 && CurrentAttackTime >= 2)
            {
                CurrentAttackTime = 0.0f;
                ChangeState("shoot");
                // CurrentWeapon.Fire();
            }
        }
    }
}
