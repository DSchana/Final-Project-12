using Artemis.Engine.Multiforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3
{
    class GameMultiform : Multiform
    {
        public GameMultiform() : base() { }
        public GameMultiform(string name) : base(name) { }

        public override void Construct(MultiformConstructionArgs args)
        {
            Console.WriteLine("LET THE GAMES BEGIN");
            throw new NotImplementedException();
        }
    }
}
