using Artemis.Engine;
using Artemis.Engine.Assets;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Half_Life_3.Entities
{
    class Sprite
    {
        /// <summary>
        /// Holds all Textures to render
        /// </summary>
        public Dictionary<string, List<Texture2D>> Textures { get; private set; }

        /// <summary>
        /// Animation state
        /// </summary>
        public string CurrentState { get; private set; }

        /// <summary>
        /// Current frame being displayed
        /// </summary>
        public float Frame { get; private set; }

        /// <summary>
        /// The rate at which Frame is changed
        /// </summary>
        public float FrameRate { get; set; }

        /// <summary>
        /// True if animation is happening
        /// </summary>
        public bool IsAnimating { get; private set; }

        /// <summary>
        /// True if the entity is attacking (only for characters)
        /// </summary>
        public bool Attacking { get; private set; }

        /// <summary>
        /// True if the sprites are always animating
        /// </summary>
        public bool AlwaysAnimating { get; private set; }

        private string[] ImageExtensions = { ".png", ".jpg", ".jpeg" };

        public Sprite()
        {
            Textures = new Dictionary<string, List<Texture2D>>();
            IsAnimating = false;
            AlwaysAnimating = false;
            Attacking = false;
            Frame = 0;
            FrameRate = 0.2f;
        }

        public void LoadImage(string path, string stateName)
        {
            string state = stateName;

            if (!Textures.ContainsKey(state))
            {
                Textures.Add(state, new List<Texture2D>());
            }

            if (ImageExtensions.Contains(Path.GetExtension(path).ToLower()))
            {
                Textures[state].Add(AssetLoader.Load<Texture2D>(path.Substring(path.IndexOf('\\') + 1), false));
            }
        }

        public void LoadSprites(string path)
        {
            string state = path.Split('\\')[path.Split('\\').Length - 1];

            if (!Textures.ContainsKey(state))
            {
                Textures[state] = new List<Texture2D>();
            }

            foreach (var imgPath in Directory.GetFiles(path))
            {
                if (ImageExtensions.Contains(Path.GetExtension(imgPath).ToLower()))
                {
                    Textures[state].Add(AssetLoader.Load<Texture2D>(imgPath, false));
                }
            }
        }

        public void LoadSprites(string path, string stateName)
        {
            string state = stateName + path.Split('\\')[path.Split('\\').Length - 1];  // state is in the form  WeaponState

            if (!Textures.ContainsKey(state))
            {
                Textures.Add(state, new List<Texture2D>());
            }

            foreach (var imgPath in Directory.GetFiles(path))
            {
                if (ImageExtensions.Contains(Path.GetExtension(imgPath).ToLower()))
                {
                    Textures[state].Add(AssetLoader.Load<Texture2D>(imgPath.Substring(imgPath.IndexOf('\\') + 1), false));
                }
            }
        }

        public void LoadDirectory(string path)
        {
            foreach (var dirPath in Directory.GetDirectories(path))
            {
                LoadSprites(dirPath, path.Split('\\')[path.Split('\\').Length - 1]);
            }
        }

        public void Update()
        {
            if (IsAnimating)
            {
                Frame += FrameRate;
            }

            if (Frame >= Textures[CurrentState].Count)
            {
                Frame = 0;

                if (!AlwaysAnimating)
                    IsAnimating = false;

                if (Attacking)
                    Console.WriteLine("DONE ATTACKING");
                    Attacking = false;
            }
        }

        public void ToggleAlwaysAnimate()
        {
            AlwaysAnimating = !AlwaysAnimating;
            if (AlwaysAnimating)
                IsAnimating = true;
        }

        public void Animate()
        {
            if (!IsAnimating)
                IsAnimating = true;
        }

        public void Animate(string stateName)
        {
            ChangeState(stateName);
            Animate();
        }

        public void ChangeState(string stateName)
        {
            if (!Textures.ContainsKey(stateName))
                throw new KeyNotFoundException(String.Format("State wiith name: '{0}' does not exist", stateName));

            if (!Attacking)
            {
                CurrentState = stateName;
                Frame = 0;
            }

            if (CurrentState.Contains("attack") && !Attacking || CurrentState.Contains("meleeattack") && !Attacking)
            {
                Attacking = true;
            }
        }

        public void Render(Vector2 position, double rotation)
        {
            ArtemisEngine.RenderPipeline.Render(Textures[CurrentState][(int)Math.Floor(Frame)], position, null, null, rotation);
        }
    }
}
