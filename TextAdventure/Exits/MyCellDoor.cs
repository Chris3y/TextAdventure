using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure.Exits
{
    public class MyCellDoor : Exit
    {
        public MyCellDoor(string name, Timeline timeline, Room room1, Room room2) : base(name, timeline)
        {
            DisplayName = "Mysterious Room Door";
            Location = "in front of you";
            Room1Location = "across the small dark room";
            Room2Location = "behind you";
            Description = "This is possibly the way to get out of wherever you are. It appears to be a very solid and heavy door with a small barred window which you attempt to peer through. It looks like there's a landing on the other side but visibility is very limited. You get the strong impression this window is meant for looking into the room, rather than out of it.";
            IsDiscovered = true;
            IsOpen = false;
            IsLocked = true;
            NoOpenReason = "The door has no visible handle on this side.";
            Room1 = room1;
            Room2 = room2;
            
        }

        protected override void AttachWorldEvents(Timeline timeline)
        {
            base.AttachWorldEvents(timeline);
            Timeline.MicrochipFirstTaken += Timeline_MicrochipFirstTaken;
        }

        protected void Timeline_MicrochipFirstTaken(object sender, EntityEventArgs e)
        {
            IsLocked = false;
            Notify("There is a <CLICKING> sound, heard from the door");
        }
    }
}
