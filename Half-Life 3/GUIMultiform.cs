using Artemis.Engine;
using Artemis.Engine.Assets;
using Artemis.Engine.Graphics;
using Artemis.Engine.Multiforms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3
{
    class GUIMultiform : Multiform
    {
        /// <summary>
        /// Half Life font used for health and ammo
        /// </summary>
        public SpriteFont Font { get; private set; }

        /// <summary>
        /// Gun crosshairs to indicate mouse position
        /// </summary>
        public Texture2D Crosshairs { get; private set; }

        /// <summary>
        /// Images to show to tell the user which weapon is equiped
        /// </summary>
        public Dictionary<string, Texture2D> GUIWeapons { get; private set; }

        /// <summary>
        /// Location on screen where GUI data is rendered
        /// </summary>
        public Vector2 FontPosition { get; private set; }

        private StringBuilder sBuilder = new StringBuilder();
        private Color FontColour = new Color(156, 137, 72);

        public GUIMultiform() : base() { }
        public GUIMultiform(string name) : base(name) { }

        public override void Construct(MultiformConstructionArgs args)
        {
            GUIWeapons = new Dictionary<string, Texture2D>();

            GUIWeapons.Add("USPMatch", AssetLoader.Load<Texture2D>("Resources/GUI/UPS", false));
            GUIWeapons.Add("MP7", AssetLoader.Load<Texture2D>("Resources/GUI/MP7", false));
            GUIWeapons.Add("SPAS12", AssetLoader.Load<Texture2D>("Resources/GUI/SPAS12", false));
            GUIWeapons.Add("Knife", AssetLoader.Load<Texture2D>("Resources/GUI/Knife", false));

            Crosshairs = AssetLoader.Load<Texture2D>(@"Resources\Crosshairs", false);
            Font = AssetLoader.Load<SpriteFont>("hl_guifont", false);
            FontPosition = new Vector2(50, 1030);

            AddRenderer(MainRenderer);
        }

        public void MainRenderer()
        {
            sBuilder.Append("HEALTH: ");
            sBuilder.Append(Game1.Freeman.Health);
            sBuilder.Append("  AMMO: ");
            sBuilder.Append(Game1.Freeman.CurrentWeapon.ClipAmmo + "/" + Game1.Freeman.CurrentWeapon.TotalAmmo);

            ArtemisEngine.RenderPipeline.RenderText(Font, sBuilder, FontPosition, FontColour);

            sBuilder.Clear();

            ArtemisEngine.RenderPipeline.Render(Crosshairs, ArtemisEngine.Mouse.PositionVector, null, null, 0, PositionOffsets.Center, originIsRelative: true);
            ArtemisEngine.RenderPipeline.Render(GUIWeapons[Game1.Freeman.CurrentWeapon.Name], Vector2.One * 15);
        }
    }
}
