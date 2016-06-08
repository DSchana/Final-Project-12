using Artemis.Engine.Assets;
using Artemis.Engine.Multiforms;
using Artemis.Engine.Forms;
using Artemis.Engine.Fixins;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Menu
{
    class MenuMultiform : Multiform
    {
        public MenuMultiform() : base() { }
        public MenuMultiform(string name) : base(name) { }

        /// <summary>
        /// Form to handle buttons on menus
        /// </summary>
        public PositionalForm ButtonForm { get; private set; }

        public List<Button> Buttons { get; private set; }

        public override void Construct(MultiformConstructionArgs args)
        {
            ButtonForm = new PositionalForm("Buttons");
            Buttons = new List<Button>();

            Buttons.Add(new Button("New Game", 100, 600, 300, 50, NewGame));    // Dimensions and location not right
            Buttons.Add(new Button("Load Game", 100, 800, 300, 50, LoadGame));  // TODO: Fix that shit
            Buttons.Add(new Button("Options", 100, 1000, 300, 50, Options));
            Buttons.Add(new Button("Quit", 100, 1200, 300, 50, Quit));

            // Attach Fixins from data in button objects
            foreach (var button in Buttons)
            {
                // Texture2D.FromStream(ArtemisEngine.RenderPipeline.GraphicsDevice, new FileStream("../../../Resources/Menu/" + button.Name + ".png", FileMode.Open))
                ButtonForm.AttachFixin(new TextureFixin(button.Name, AssetLoader.Load<Texture2D>("Resources\\Menu\\" + button.Name + ".png", false), new PositionalForm(button.Position)));
            }

            SetUpdater(MainUpdater);
            SetRenderer(MainRenderer);

            AddForm(ButtonForm);
        }

        private void MainUpdater()
        {
            foreach (var button in Buttons)
            {
                button.OnButtonReleased();
            }
        }

        private void MainRenderer()
        {
            ButtonForm.Render();
        }

        // Button click actions
        private void NewGame()
        {
            StreamWriter newFile = new StreamWriter(Directory.GetCurrentDirectory() + "\\hl_1.hlsave");

            newFile.WriteLine(0 + " " + 0);  // Coords not right
            newFile.WriteLine(0);  // Rotation

            newFile.Close();
        }

        private void LoadGame()
        {
            StreamReader loadFile = new StreamReader(Directory.GetCurrentDirectory() + "\\hl_1.hlsave");

            int counter = 0;
            string line;
            while ((line = loadFile.ReadLine()) != null)
            {
                switch (counter)
                {
                    case 0:
                        string[] coords = line.Split(' ');
                        Game1.Freeman.WorldPosition = new Vector2((float)Convert.ToDecimal(coords[0]), (float)Convert.ToDecimal(coords[1]));
                        counter++;
                        break;

                    case 1:
                        Game1.Freeman.Rotation = (float)Convert.ToDecimal(line);
                        counter++;
                        break;

                    default:
                        break;
                }
            }

            loadFile.Close();
        }

        private void Options()
        {
            throw new NotImplementedException();
        }

        private void Quit()
        {
            Environment.ExitCode = 0;
            Environment.Exit(Environment.ExitCode);
        }
    }
}
