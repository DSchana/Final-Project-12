using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Story
{
    class StoryManager
    {
        /// <summary>
        /// Flags used to tell the story
        /// </summary>
        public Dictionary<string, Flag> Flags { get; private set; }

        public StoryManager()
        {
            Flags = new Dictionary<string, Flag>();

            StreamReader flagReader = new StreamReader(Directory.GetCurrentDirectory() + @"\hl_flags.flags");

            string line;
            List<string> flags = new List<string>();

            while ((line = flagReader.ReadLine()) != null)
            {
                flags.Add(line);
            }

            flagReader.Close();

            Flags.Add(flags[0], new Flag(flags[0], flags[1], true));

            for (int i = 1; i < flags.Count - 1; i++)
            {
                Flags.Add(flags[i], new Flag(flags[i], flags[i+1]));
            }
        }

        public void Next()
        {
            foreach (KeyValuePair<string, Flag> flag in Flags)
            {
                if (flag.Value.IsActive)
                {
                    flag.Value.Advance();
                }
            }
        }

        public void TriggerFlag()
        {
            foreach (KeyValuePair<string, Flag> flag in Flags)
            {
                if (flag.Value.IsActive)
                {
                    flag.Value.Trigger();
                }
            }
        }
    }
}
