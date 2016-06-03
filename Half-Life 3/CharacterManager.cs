using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3
{
    /// <summary>
    /// Stores all characters and determines how damage is dealt.
    /// Used to allow weapons to be hitscan rather than projectiles.
    /// </summary>
    class CharacterManager
    {
        /// <summary>
        /// List of Characters to handle
        /// </summary>
        public Dictionary<string, Character> Characters { get; private set; }

        public CharacterManager()
        {
            Characters = new Dictionary<string, Character>();
        }

        public void Add(Character character)
        {
            Characters.Add(character.Name, character);
        }

        public void Add(PlayableCharacter character)
        {
            Characters.Add(character.Name, character);
        }

        public void Kill(string name)
        {
            try
            {
                Characters.Remove(name);
            }
            catch (KeyNotFoundException e)
            {
                throw new KeyNotFoundException(String.Format("'{0}' not a character. \nError at: \n'{1}'", name, e.Source));
            }
        }

        public void Damage()
        {
            // Playable character to others
        }
    }
}
