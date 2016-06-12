using Artemis.Engine;
using Artemis.Engine.Assets;
using Artemis.Engine.Fixins;
using Artemis.Engine.Forms;
using Artemis.Engine.Multiforms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Buttons displayed on this menu
        /// </summary>
        public List<Button> Buttons { get; private set; }

        /// <summary>
        /// Background of menu
        /// </summary>
        public Texture2D Background { get; private set; }

        /// <summary>
        /// Half-Life 3 Title Image
        /// </summary>
        public Texture2D Title { get; private set; }

        /// <summary>
        /// Cursor image
        /// </summary>
        public Texture2D Cursor { get; private set; }

        private Random rnd = new Random();

        public override void Construct(MultiformConstructionArgs args)
        {
            // ButtonForm = new PositionalForm("Buttons");
            // TODO: Change the range
            Background = AssetLoader.Load<Texture2D>(@"Resources\Menu\background " + rnd.Next(1, 5) + ".jpg", false);
            Cursor = AssetLoader.Load<Texture2D>(@"Resources\Cursor.png", false);
            Title = AssetLoader.Load<Texture2D>(@"Resources\Menu\Title.png", false);

            Buttons = new List<Button>();

            Buttons.Add(new Button("New Game", 100, 600, 300, 50, NewGame));    // Dimensions and location not right
            Buttons.Add(new Button("Load Game", 100, 650, 300, 50, LoadGame));  // TODO: Fix that shit
            Buttons.Add(new Button("Options", 100, 700, 300, 50, Options));
            Buttons.Add(new Button("Quit", 100, 750, 300, 50, Quit));

            /*
            // Attach Fixins from data in button objects
            foreach (var button in Buttons)
            {
                // Texture2D.FromStream(ArtemisEngine.RenderPipeline.GraphicsDevice, new FileStream("..\\..\\..\\Resources\\Menu\\" + button.Name + ".png", FileMode.Open))
                // AssetLoader.Load<Texture2D>("Resources\\Menu\\" + button.Name + ".png", false)
                ButtonForm.AttachFixin(new TextureFixin(button.Name, AssetLoader.Load<Texture2D>(@"Resources\Menu\" + button.Name + ".png", false), new PositionalForm(button.Position)));
            }
            */

            SetUpdater(MainUpdater);
            SetRenderer(MainRenderer);

            // AddForm(ButtonForm);
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
            ArtemisEngine.RenderPipeline.Render(Background, Vector2.Zero);

            foreach (var button in Buttons)
            {
                button.Show();
            }

            ArtemisEngine.RenderPipeline.Render(Title, new Vector2(150, 520));
            ArtemisEngine.RenderPipeline.Render(Cursor, ArtemisEngine.Mouse.Position.ToVector2());
        }

        // Button click actions
        private void NewGame()
        {
            StreamWriter newFile = new StreamWriter(Directory.GetCurrentDirectory() + "\\hl_1.hlsave");

            newFile.WriteLine(0 + " " + 0);  // Coords not right
            newFile.WriteLine(0);  // Rotation

            newFile.Close();

            ArtemisEngine.MultiformManager.Activate(this, "Game");
            ArtemisEngine.MultiformManager.Deactivate(this);
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

            // TODO: Activate Game Multiform
        }

        private void Options()
        {
            throw new NotImplementedException();
        }

        private void Quit()
        {
            Console.WriteLine("QUIT");
            Environment.ExitCode = 0;
            Environment.Exit(Environment.ExitCode);
        }
    }
}
