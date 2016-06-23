using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Microchip : Item
    {
        public Microchip(string name, Timeline timeline) : base(name, timeline)
        {
            DisplayName = "Microchip";
            Description = "It's hard to examine while it's still stuck in your neck";
            Location = "protruding out of your neck";
            IsDiscovered = false;
        }
        
        protected override void AttachLocalEvents()
        {
            Taken += Microchip_Taken;
            Used += Microchip_Used;

        }
        protected override void AttachWorldEvents(Timeline timeline)
        {
            Timeline.PlayerFirstInspected += Timeline_PlayerFirstInspected;
            Timeline.MicrochipFirstTaken += Timeline_MicrochipFirstTaken;
        }

        void Microchip_Used(object sender, EntityEventArgs e)
        {
            Notify("It isn't doing anything.");
        }
        void Microchip_Taken(object sender, EntityEventArgs e)
        {

            if (TakenCount == 0)
            {
                Timeline.OnMicrochipFirstTaken();
            }
        }

        void Timeline_PlayerFirstInspected(object sender, EntityEventArgs e)
        {
            this.IsTakable = true;
        }
        void Timeline_MicrochipFirstTaken(object sender, EntityEventArgs e)
        {
            Description = "A computer chip that you dug out of your neck. It must be used for tracking and measuring biological functions.";
            Notify("<Subject's vital signs lost>: Pronounced Deceased.");
            IsUsable = true;
        }
        

    }
}
