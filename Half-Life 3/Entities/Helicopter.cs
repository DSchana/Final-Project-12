using Artemis.Engine;
using Artemis.Engine.Assets;
using Artemis.Engine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Entities
{
    class Helicopter : Entity
    {
        /// <summary>
        /// Body image of helicopter
        /// </summary>
        public new Texture2D Body { get; private set; }

        /// <summary>
        /// Rotor blades of chopper
        /// </summary>
        public Texture2D Rotor { get; private set; }

        /// <summary>
        /// Rotation point of Body and rotor
        /// </summary>
        public Vector2 Pivot { get; private set; }

        private double BladeRotation = 0.0;
        private float RotationAcceleration = 0.001f;
        private float RotationSpeed = 0.0f;
        private float Acceleration = 0.3f;
        private float Speed = 0.0f;

        public Helicopter(string name, int x, int y) : base(name, x, y)
        {
            WorldPosition = new Vector2(x, y);
            Pivot = new Vector2(0.5f, 0.20277f);

            Type = EntityType.Helicopter;

            Health = 500;

            Body = AssetLoader.Load<Texture2D>(@"Resources\Helicopter\Body", false);
            Rotor = AssetLoader.Load<Texture2D>(@"Resources\Helicopter\Blades.png", false);

            ScreenPosition = new Vector2(ArtemisEngine.DisplayManager.WindowResolution.Width / 2, ArtemisEngine.DisplayManager.WindowResolution.Height / 2);

            /*
            AddUpdater(Rotate);
            AddUpdater(Move);
            */
        }

        public void Rotate()
        {
            if (ArtemisEngine.Keyboard.IsHeld(Keys.A))
            {
                RotationSpeed += RotationAcceleration;
                if (RotationSpeed > 0.05f)
                {
                    RotationSpeed = 0.05f;
                }
            }
            if (ArtemisEngine.Keyboard.IsHeld(Keys.D))
            {
                RotationSpeed -= RotationAcceleration;
                if (RotationSpeed < -0.05f)
                {
                    RotationSpeed = -0.05f;
                }
            }

            BladeRotation += 10.0f;
            if (BladeRotation > 360.0f)
            {
                BladeRotation = 0.0f;
            }

            Rotation += RotationSpeed;
        }

        public void Move()
        {
            if (ArtemisEngine.Keyboard.IsHeld(Keys.W))
            {
                Speed += Acceleration;
            }
            if (ArtemisEngine.Keyboard.IsHeld(Keys.S))
            {
                Speed -= Acceleration;
            }

            if (Speed > 5)
            {
                Speed = 5;
            }
            else if (Speed < -5)
            {
                Speed = -5;
            }

            Vector2 NewWorldPosition = WorldPosition;

            NewWorldPosition.X += (float)(Speed * Math.Cos(Rotation));
            NewWorldPosition.Y += (float)(Speed * Math.Sin(Rotation));

            WorldPosition = NewWorldPosition;
        }

        public void Draw()
        {
            ArtemisEngine.RenderPipeline.Render(Body, ScreenPosition, null, null, Rotation + 90, Pivot, originIsRelative: true);
            ArtemisEngine.RenderPipeline.Render(Rotor, ScreenPosition, null, null, BladeRotation, PositionOffsets.Center, originIsRelative: true);
        }
    }
}
