using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Item : Entity
    {
        public Item(string name, Timeline timeline) : base(name, timeline)
        {
            ThingType = ThingTypes.Item;
        }

        private Room currentRoom;

        public new Room CurrentRoom
        {
            get
            {
                if (Owner != null)
                {
                    return Owner.CurrentRoom;
                }
                else
                {
                    return currentRoom;
                }
            }
            private set { currentRoom = value; }
        }

    }
}
