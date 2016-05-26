using Artemis.Engine;
using Artemis.Engine.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3
{
    class Button : Form
    {
        /// <summary>
        /// Button position on screen
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        /// True if the mouse is hovering over this Button
        /// </summary>
        public bool IsHovering { get; private set; }

        /// <summary>
        /// Mouse input for buttons
        /// </summary>
        public MouseInput MouseIn { get; private set; }

        /// <summary>
        /// Collision box for Button
        /// </summary>
        public Rectangle BoundingBox { get; private set; }


        public Button(string lable, int x, int y, int width, int height)
        {
            MouseIn = new MouseInput();
            BoundingBox = new Rectangle(x, y, width, height);
        }

        public Button(string lable, Rectangle boundingBox)
        {
            MouseIn = new MouseInput();
            BoundingBox = boundingBox;
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }

        public void OnButtonClicked(Action action)
        {
            if (MouseIn.IsClicked(MouseButton.Left) && BoundingBox.Contains(MouseIn.PositionVector))
            {
                action();
            }
        }

        public void OnButtonReleased(Action action)
        {
            if (MouseIn.IsReleased(MouseButton.Left) && BoundingBox.Contains(MouseIn.PositionVector))
            {
                action();
            }
        }
    }
}
