using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Artemis.Engine;
using Artemis.Engine.Graphics;

namespace Half_Life_3
{
    /// <summary>
    /// A genaric class for playable and non-playable
    /// characters.
    /// </summary>
    class Character : Form
    {
        /// <summary>
        /// Boolean value to state if the user can
        /// play as this character
        /// </summary>
        public bool IsPlayable { get; private set; }

        public string CharacterName { get; private set; } = null;

        /// <summary>
        /// The current state in which the character
        /// is in.
        /// </summary>
        public AnimationState CurrentState { get; private set; }

        /// <summary>
        /// Render character to the screen
        /// </summary>
        public override void Render()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Base attack actions including animation.
        /// </summary>
        public void Attack()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Change the state in which the animation
        /// will render images from.
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(AnimationState state)
        {
            CurrentState = state;
        }

        public void SetName(string name)
        {
            if (CharacterName != null)
            {
                CharacterName = name;
                return;
            }
            throw new Exception("Character name cannot be changed after it has been set.");
        }
    }
}
