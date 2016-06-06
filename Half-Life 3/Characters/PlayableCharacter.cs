using Artemis.Engine.Graphics.Animation;
using Artemis.Engine.Input;
using Half_Life_3.Weapons;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Characters
{
    class PlayableCharacter : Character
    {
        private KeyboardInput keyIn = new KeyboardInput();
        private MouseInput mouseIn = new MouseInput();

        // TODO: Populate
        public List<Weapon> Weapons { get; private set; }

        public PlayableCharacter(string name, string AnimationFileName) : base(name)
        {
            Name = name;
            IsPlayable = true;
            Weapons = new List<Weapon>();
            AAMLReader = new AAMLFileReader(AnimationFileName);

            SetMaxHealth(100);

            AddUpdater(Rotate);
            AddUpdater(Move);
            AddUpdater(Attack);
        }

        private void Rotate()
        {
            Vector2 direction = mouseIn.PositionVector - ScreenPosition;
            direction.Normalize();

            Rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        private void Move()
        {
            bool isMoving = false;
            Vector2 NewWorldPosition = new Vector2(WorldPosition.X, WorldPosition.Y);

            if (keyIn.IsHeld(Keys.W))
            {
                isMoving = true;
                NewWorldPosition.X += (float)(Speed * Math.Cos(Rotation));
                NewWorldPosition.Y += (float)(Speed * Math.Sin(Rotation));
            }
            if (keyIn.IsHeld(Keys.A))
            {
                isMoving = true;
                NewWorldPosition.X -= (float)(Speed * Math.Cos(Rotation + 90));
                NewWorldPosition.Y -= (float)(Speed * Math.Sin(Rotation + 90));
            }
            if (keyIn.IsHeld(Keys.S))
            {
                isMoving = true;
                NewWorldPosition.X -= (float)(Speed * Math.Cos(Rotation));
                NewWorldPosition.Y -= (float)(Speed * Math.Sin(Rotation));
            }
            if (keyIn.IsHeld(Keys.D))
            {
                isMoving = true;
                NewWorldPosition.X += (float)(Speed * Math.Cos(Rotation + 90));
                NewWorldPosition.Y += (float)(Speed * Math.Sin(Rotation + 90));
            }

            if (isMoving)
            {
                ChangeState("Move");
            }
            else
            {
                ChangeState("Idle");
            }

            WorldPosition = NewWorldPosition;
        }

        private void Attack()
        {
            ChangeState("Attack");
            if (mouseIn.IsClicked(MouseButton.Left) && CurrentWeapon.ClipAmmo > 0)
            {
                CurrentWeapon.Fire();
            }
            else if (mouseIn.IsClicked(MouseButton.Right))
            {
                CurrentWeapon.Fire(DamageType.Melee);
            }
        }
    }
}