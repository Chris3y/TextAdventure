using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Exit : Entity
    {
        public Exit(string name, Timeline timeline) : base(name, timeline)
        {
            Rooms = new Room[] { Room1, Room2 };
            ThingType = ThingTypes.Exit;
            IsUsable = true;
        }

        public string Room1Location { get; set; }
        public string Room2Location { get; set; }
        public Room Room1 { get; set; }
        public Room Room2 { get; set; }
        public Room[] Rooms { get; set; }
        
        public string GetLocation(Room room)
        {
            if (this.IsDiscovered)
            {
                if (room.Equals(Room1))
                {
                    return this.Room1Location;
                }
                else
                {
                    return this.Room2Location;
                }
            }

            return string.Empty;
        }
        public override ActionResult Use(Actor doer)
        {
            var result = new ActionResult();

            if (!IsUsable)
            {
                result.IsSuccessful = false;
                result.Outcome = string.Format("You can't use {0}. {1}", DisplayName, NoUseReason);

                OnUsed(result);
                return result;
            }

            if (IsOpen)
            {
                var otherRoom = doer.CurrentRoom.GetJoiningRoom(this);
                doer.CurrentRoom = otherRoom;                
                result.IsSuccessful = true;
                result.Outcome = string.Format("{0} used the {1}.", doer.DisplayName, DisplayName);

                OnUsed(result);
                doer.CurrentRoom.Enter(doer);
                return result;
            }
            else
            {
                result.IsSuccessful = false;
                result.Outcome = string.Format("{0} is not open.", DisplayName);

                OnUsed(result);
                return result;
            }
        }

    }
}
