using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class EntityEventArgs : EventArgs
    {
        public EntityEventArgs()
        {
        }

        public EntityEventArgs(ActionResult result)
        {
            Result = result;
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public ActionResult Result { get; set; }
    }
}
