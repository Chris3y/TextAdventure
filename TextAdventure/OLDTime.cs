using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class OLDTime
    {
        public OLDTime(Space space)
        {
            Space = space;
            Subscribe();
        }

        public Space Space { get; set; }

        private void Subscribe()
        {
            //Space.Player.Inspected += Player_Inspected;
            //Space.FindItemByName("MCROCHIP").Taken += Microchip_Taken;
            //Space.FindExitByName("MYCELDR1").Opened += MyCellDoor_Opened;
            //Space.FindItemByName("SMLPLAQE1").Inspected += SmallPlaque_Inspected;
        }

        private void SmallPlaque_Inspected(object sender, EntityEventArgs e)
        {
            var item = (sender as Entity);
            if (item.InspectedCount <= 0)
            {                
                Room room = Space.FindRoomByName("MYCELL01");
                if (room != null)
                {
                    //Output.Say("You", "Wait... I'm a prisoner?");
                    room.DisplayName = "My Cell";
                    Space.FindExitByName("MYCELDR1").DisplayName = "My Cell Door";
                    Console.WriteLine("(Mysterious Room now known as \"My Cell\".)");
                    Console.WriteLine("(Mysterious Room Door now known as \"My Cell Door\".)");
                }
            }
        }

        private void MyCellDoor_Opened(object sender, EntityEventArgs e)
        {
            //Output.Announce("You grasp the bars in the windows slowly heave open the door. As it opens, light from the landing makes the room slighly more visible, though there isn't much to see.");
            Space.FindRoomByName("MYCELL01").Description = "The room is very bare. There's really not much to see.";
        }

        private void Microchip_Taken(object sender, EntityEventArgs e)
        {
            var chip = sender as Item;
            if (chip.TakenCount == 0)
            {
                //chip.Description = "A computer chip you dug out of your neck. It must be used for tracking and measuring biological functions.";
                Space.Player.Description = "Clothed in greyish overalls, there is a tag on the left side of your chest which reads \"19771980\". You have an open wound on your neck from where you removed the Microchip.";
                Space.FindExitByName("MYCELDR1").IsLocked = false;
                //Output.Say("<Voice of Microchip>","Subject 19771980: Deceased.");
                //Output.Announce("<A clicking sound was heard from the door.>");
            }
        }

        private void Player_Inspected(object sender, EntityEventArgs e)
        {
            if ((sender as Entity).InspectedCount == 0)
            {
                var item = Space.FindItemByName("MCROCHIP");
                item.IsDiscovered = true;
                item.IsTakable = true;
            }
        }
    }
}
