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
        public float FrameRate { get; private set; }

        /// <summary>
        /// True if animation is happening
        /// </summary>
        public bool IsAnimating { get; private set; }

        private string[] ImageExtensions = { "png", "jpg", "jpeg" };

        public Sprite()
        {
            Textures = new Dictionary<string, List<Texture2D>>();
            Frame = 0;
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

            if (Frame >= Textures[CurrentState].Count && IsAnimating)
            {
                Frame = 0;
                IsAnimating = false;
            }
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

            CurrentState = stateName;
        }

        public void Render(Vector2 position, double rotation)
        {
            ArtemisEngine.RenderPipeline.Render(Textures[CurrentState][(int)Frame], position, null, null, rotation);
        }
    }
}
