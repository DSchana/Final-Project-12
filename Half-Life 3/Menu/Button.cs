using Artemis.Engine;
using Artemis.Engine.Assets;
using Artemis.Engine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Menu
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
        /// Collision box for Button
        /// </summary>
        public Rectangle BoundingBox { get; private set; }

        /// <summary>
        /// Image displayed of button
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// The action called when button is triggered
        /// </summary>
        public Action ActivatedAction { get; private set; }

        public Button(string lable, int x, int y, int width, int height) : base(lable)
        {
            BoundingBox = new Rectangle(x, y, width, height);
            Position = new Vector2(x, y);
            Texture = AssetLoader.Load<Texture2D>(@"Resources\Menu\" + lable + ".png", false);
        }

        public Button(string lable, Rectangle boundingBox) : base(lable)
        {
            BoundingBox = boundingBox;
            Position = new Vector2(BoundingBox.X, BoundingBox.Y);
            Texture = AssetLoader.Load<Texture2D>(@"Resources\Menu\" + lable + ".png", false);
        }

        public Button(string lable, int x, int y, int width, int height, Action action) : base(lable)
        {
            BoundingBox = new Rectangle(x, y, width, height);
            Position = new Vector2(x, y);
            ActivatedAction = action;
            Texture = AssetLoader.Load<Texture2D>(@"Resources\Menu\" + lable + ".png", false);
        }

        public Button(string lable, Rectangle boundingBox, Action action) : base(lable)
        {
            BoundingBox = boundingBox;
            Position = new Vector2(BoundingBox.X, BoundingBox.Y);
            ActivatedAction = action;
            Texture = AssetLoader.Load<Texture2D>(@"Resources\Menu\" + lable + ".png", false);
        }

        public void AddAction(Action action)
        {
            ActivatedAction = action;
        }

        public void OnButtonClicked()
        {
            if (ArtemisEngine.Mouse.IsClicked(MouseButton.Left) && BoundingBox.Contains(ArtemisEngine.Mouse.Position))
            {
                ActivatedAction();
            }
        }

        public void OnButtonReleased()
        {
            if (ArtemisEngine.Mouse.IsReleased(MouseButton.Left) && BoundingBox.Contains(ArtemisEngine.Mouse.Position))
            {
                ActivatedAction();
            }
        }

        public void Show()
        {
            ArtemisEngine.RenderPipeline.Render(Texture, Position);
        }
    }
}
