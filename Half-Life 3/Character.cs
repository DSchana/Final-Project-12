using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis.Engine;
using Artemis.Engine.Graphics;
using Artemis.Engine.Graphics.Animation;
using Microsoft.Xna.Framework;

namespace Half_Life_3
{
    /// <summary>
    /// A genaric class for playable and non-playable
    /// characters.
    /// </summary>
    class Character : PhysicalForm 
    {
        /// <summary>
        /// Boolean value to state if the user can
        /// play as this character
        /// </summary>
        public bool IsPlayable = false;

        /// <summary>
        /// Character health (0-MaxHealth)
        /// </summary>
        public int Health { get; private set; }

        /// <summary>
        /// Max health of a character. Set automatically
        /// to be the initial health provided.
        /// </summary>
        public int MaxHealth { get; private set; } = -1;

        /// <summary>
        /// AAMLReader to parse the aaml file associated
        /// with character.
        /// </summary>
        public AAMLFileReader AAMLReader;

        /// <summary>
        /// The rotation of the character
        /// </summary>
        public float Rotation;

        /// <summary>
        /// Speed of character
        /// 5 is standard speed for most
        /// </summary>
        public int Speed = 5;

        public Character(string name) : base(name)
        {
            AddRenderer(Show);
        }

        /// <summary>
        /// Render character to the screen
        /// </summary>
        public void Show()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Change the state in which the animation
        /// will render images from.
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(string state)
        {
            AAMLReader.Map.SetState(state);
        }

        public void AddHealth(int health)
        {
            Health += health;
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                Kill();
            }
        }

        public void SetMaxHealth(int health)
        {
            if (MaxHealth == -1)
            {
                MaxHealth = health;
                Health = health;
            }
        }
    }
}
