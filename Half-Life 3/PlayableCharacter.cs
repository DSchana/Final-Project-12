using Artemis.Engine.Graphics.Animation;
using Artemis.Engine.Input;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3
{
    class PlayableCharacter : Character
    {
        public KeyboardInput KeyIn { get; private set; }
        public MouseInput MouseIn { get; private set; }

        public PlayableCharacter(string name, string AnimationFileName) : base()
        {
            Name = name;
            IsPlayable = true;
            AAMLReader = new AAMLFileReader(AnimationFileName);

            SetMaxHealth(100);

            KeyIn = new KeyboardInput();
            MouseIn = new MouseInput();

            AddUpdater(Rotate);
            AddUpdater(Move);
            AddUpdater(Attack);
        }

        private void Rotate()
        {
            Vector2 direction = MouseIn.PositionVector - ScreenPosition;
            direction.Normalize();

            Rotation = (float)Math.Atan2(direction.Y, direction.X);
        }

        private void Move()
        {
            bool isMoving = false;
            Vector2 NewWorldPosition = new Vector2(WorldPosition.X, WorldPosition.Y);

            if (KeyIn.IsHeld(Keys.W))
            {
                isMoving = true;
                NewWorldPosition.X += (float)(Speed * Math.Cos(Rotation));
                NewWorldPosition.Y += (float)(Speed * Math.Sin(Rotation));
            }
            if (KeyIn.IsHeld(Keys.A))
            {
                isMoving = true;
                NewWorldPosition.X -= (float)(Speed * Math.Cos(Rotation + 90));
                NewWorldPosition.Y -= (float)(Speed * Math.Sin(Rotation + 90));
            }
            if (KeyIn.IsHeld(Keys.S))
            {
                isMoving = true;
                NewWorldPosition.X -= (float)(Speed * Math.Cos(Rotation));
                NewWorldPosition.Y -= (float)(Speed * Math.Sin(Rotation));
            }
            if (KeyIn.IsHeld(Keys.D))
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
        }
    }
}