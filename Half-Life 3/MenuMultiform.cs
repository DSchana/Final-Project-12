using Artemis.Engine.Multiforms;
using Artemis.Engine.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Half_Life_3
{
    class MenuMultiform : Multiform
    {
        public MenuMultiform() : base() { }
        public MenuMultiform(string name) : base(name) { }

        public override void Construct(MultiformConstructionArgs args)
        {
            SetUpdater(MainUpdater);
            SetRenderer(MainRenderer);
        }

        private void MainUpdater()
        {

        }

        private void MainRenderer()
        {

        }
    }
}
