using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Player : Actor
    {
        public Player(string name, Timeline timeline) : base(name, timeline)
        {

        }

        protected override void AttachLocalEvents()
        {
            this.Inspected += Player_Inspected;
        }

        private void Player_Inspected(object sender, EntityEventArgs e)
        {
            if (InspectedCount == 0)
            {
                Timeline.OnPlayerFirstInspected();
            }
        }
        
    }
}
