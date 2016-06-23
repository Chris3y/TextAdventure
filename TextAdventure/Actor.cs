using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Actor : Entity
    {
        public Actor(string name, Timeline timeline) : base(name, timeline)
        {
            Inventory = new List<Item>();
            ThingType = ThingTypes.Player;
        }

        public List<Item> Inventory { get; set; }
    }
}
