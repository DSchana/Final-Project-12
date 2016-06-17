using Half_Life_3.Entities.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3.Story
{
    class Flag
    {
        /// <summary>
        /// Name of flag
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Flag to activate after this one
        /// </summary>
        public string NextFlag { get; private set; }

        /// <summary>
        /// True if story is at this flag's chronological location
        /// </summary>
        public bool IsActive { get; set; }

        private int enemySpawnTimes = 0;

        public Flag(string name, string nextFlag, bool isActive = false)
        {
            Name = name;
            NextFlag = nextFlag;
            IsActive = isActive;
        }

        public void Advance()
        {
            if (NextFlag.ToLower() != "end")
            {
                Game1.StoryManager.Flags[NextFlag].IsActive = true;
                IsActive = false;
            }
        }

        public void Trigger()
        {
            if (Name.ToLower() == "funeral")
            {
                /*
                Game1.Alyx.Say("I can't believe he's gone.");
                Game1.Alyx.Say("My father did not deserve this");
                Game1.Alyx.Say("Eli was a good man");
                Game1.Alyx.Say("Come on Gordon, we need to go and finish the job");
                Game1.Alyx.Say("The Combine won't stay at rest for long, they will find a way to reopen the portal");
                Game1.Alyx.Say("Unless we stop them at their source");
                Game1.Alyx.Say("We will be under constant threat");
                Game1.Alyx.Say("Let's head over to the hanger and find the Borealis");
                Game1.Alyx.Say("Oh, By the way");
                Game1.Alyx.Say("I've taken the liberty and upgraded your HEV Hazard Suit");
                */
                Game1.DialogueManager.Write("Hazard Suit Vocal Aid Module", "Head over the hanger east of your location");
                Game1.StoryManager.Next();
            }
            else if (Name.ToLower() == "overwatchbattle")
            {
                if (Game1.Freeman.WorldPosition.X > 1600)
                {
                    Game1.Alyx.Say("Gordon, Watch out. Combine soldiers");
                    if (!Game1.EntityManager.Entities.ContainsKey("Combine0") &&
                        !Game1.EntityManager.Entities.ContainsKey("Combine1") &&
                        !Game1.EntityManager.Entities.ContainsKey("Combine2") &&
                        !Game1.EntityManager.Entities.ContainsKey("Combine3") &&
                        !Game1.EntityManager.Entities.ContainsKey("Combine4"))
                    {
                        enemySpawnTimes++;
                        for (int i = 0; i < 5; i++)
                        {
                            Game1.EntityManager.Add(new CombineSoldier("Combine" + i, CombineType.OverwatchSoldier, 1600 + i * 150, 1300));
                        }
                    }
                }
                if (enemySpawnTimes == 3)
                {
                    Game1.StoryManager.Next();
                }
            }
            else if (Name.ToLower() == "hanger")
            {
                Game1.Alyx.Say("Well, here we are.");
                Game1.Alyx.Say("This is where it happen.");
                Game1.Alyx.Say("Let's keep moving. Get in the chopper Gordon");
            }
        }
    }
}
