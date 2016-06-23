using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class World
    {
        public World(Timeline timeline)
        {
            Timeline = timeline;
            
            BuildAllThings();
            BuildPlayer();
            BuildItems();
            BuildRooms();
            BuildExits();
            
            InitializePlayer();
            InitializeItems();
            InitializeExits();
            InitializeRooms();

            Narrator = new Narrator(Timeline);
            InitializeNarrator();     
        }

        private void InitializeNarrator()
        {
            foreach (Entity entity in AllThings)
            {
                Narrator.ListentTo(entity);
            }
        }

        public Timeline Timeline { get; set; } = new Timeline();
        public Narrator Narrator { get; set; } 
        public List<Entity> AllThings { get; set; }
        public Player Player { get; set; }
        public Entity FindThingByNameAndType(string name, ThingTypes type, bool cast= false)
        {
            var thing = AllThings.Find(t => t.Name.Equals(name) && t.ThingType == type);
            return thing;
        }
        public Room FindRoomByName(string name)
        {
            var room = AllThings.Find(r => r.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase) && r.ThingType == ThingTypes.Room);

            return room as Room;         
        }
        public Item FindItemByName(string name)
        {
            var item = AllThings.Find(i => i.Name.Equals(name) && i.ThingType == ThingTypes.Item);

            return item as Item;
        }
        public Exit FindExitByName(string name)
        {
            var item = AllThings.Find(i => i.Name.Equals(name) && i.ThingType == ThingTypes.Exit);

            return item as Exit;
        }

        void BuildAllThings()
        {
            AllThings = new List<Entity>();
        }
        void BuildPlayer()
        {
            Player = new Player("Player01", Timeline);
            AllThings.Add(Player);
        }
        void BuildItems()
        {            
            AllThings.Add(new Microchip("MCROCHIP", Timeline));         
            AllThings.Add(new Item("SMLPLAQE1", Timeline));
            AllThings.Add(new Item("SMLMATT1", Timeline));
            AllThings.Add(new Item("SMLHANDL", Timeline));
            AllThings.Add(new Item("METALBX1", Timeline));
        }
        void BuildRooms()
        {
            AllThings.Add(new Room("MYCELL01", Timeline));
            AllThings.Add(new Room("SMLLANDN", Timeline));
            AllThings.Add(new Room("MANLANDN", Timeline));
        }
        void BuildExits()
        {
            AllThings.Add(new Exits.MyCellDoor("MYCELDR1", Timeline, FindRoomByName("MYCELL01"), FindRoomByName("SMLLANDN")));
            AllThings.Add(new Exit("FOTBRDG1", Timeline));
        }

        void InitializePlayer()
        {
            Player.DisplayName = "<unknown>";
            Player.Description = "As you look down at your body, you discover you're clothed in greyish overalls. There is a tag on the left side of your chest which reads \"19771980\". Your thunderous headache only briefly gives way to the pain of your arms and legs. You can barely clench a fist. \"Am I a patient? A prisoner?, What's happening!?\" you say to yourself, while feeling round your battered body, trying to understand the extent and source of the damage. Your neck is paticularly sore, as you try to inspect it with your filthy hands, there's a sharp pain forcing you to recoil for a moment. You take a deep breath and try again, you can feel an open flesh wound but there is something metalic in there...";
            Player.CurrentRoom = FindRoomByName("MYCELL01");
            Player.Entities.Add(FindItemByName("MCROCHIP"));
            Player.IsDiscovered = true;
        }
        void InitializeRooms()
        {
            Room room;

            // PRISON CELL
            // ================================================================
            room = FindRoomByName("MYCELL01");
            room.DisplayName = "Mysterious Room";
            room.Description = "It is in almost complete darkness except for light coming through a barred window on what appears to be the room door.";
            room.IsDiscovered = true;
            
            // Exits
            room.Entities.Add(FindExitByName("MYCELDR1"));

            // Items            
            room.Entities.Add(FindItemByName("SMLHANDL"));
            room.Entities.Add(FindItemByName("METALBX1"));

            // SMALL LANDING
            // ================================================================
            room = FindRoomByName("SMLLANDN");
            room.DisplayName = "Small Landing";
            room.Description = "The walkway clangs under your footsteps and the noises echo off the cavernous walls and ceiling, which seem to be made of rock with a red-ish hue. There's a quiet roar of some sort of machinery below. A cold metal hand-rail is all that stands between you and a very, very long fall.";
            room.IsDiscovered = false;

            // Items
            room.Entities.Add(FindItemByName("SMLPLAQE1"));

            // Exits
            room.Entities.Add(FindExitByName("MYCELDR1"));
            room.Entities.Add(FindExitByName("FOTBRDG1"));

            // MAIN LANDING
            // ================================================================
            room = FindRoomByName("MANLANDN");
            room.DisplayName = "Main Landing";
            room.Description = "The carvenous walls span further and further away. The main landing contains more of the same doors you just came out of.";
            room.IsDiscovered = false;

            // Items
            room.Entities.Add(FindItemByName("SMLPLAQE1"));

            // Exits
            room.Entities.Add(FindExitByName("FOTBRDG1"));


        }
        void InitializeExits()
        {
            Exit exit;

            // FOOT BRIDGE
            exit = FindExitByName("FOTBRDG1");
            exit.DisplayName = "Foot Bridge";
            exit.Room1Location = "to the south";
            exit.Room1Location = "to the north";
            exit.IsDiscovered = true;
            exit.IsOpen = true;
            exit.Room1 = FindRoomByName("SMLLANDN");
            exit.Room2 = FindRoomByName("MANLANDN");

        }
        void InitializeItems()
        {
            Item item;

            // MICROCHIP
            //item = FindItemByName("MCROCHIP");
            //item.DisplayName = "Microchip";
            //item.Description = "It's hard to examine while it's still stuck in your neck";
            //item.Location = "protruding out of your neck";
            //item.IsDiscovered = false;

            // SMALL PLAQUE
            item = FindItemByName("SMLPLAQE1");            
            item.DisplayName = "Small Plaque";
            item.Description = "It reads: \"Prisoner: 19771980\"";
            item.Location = "near the entrance of the room you just left";
            item.IsDiscovered = true;

            // SQUARE METAL BOX
            item = FindItemByName("METALBX1");
            item.DisplayName = "Metal Box";
            item.Description = "A knee-high metal box that has a strong smell of chemicals.";
            item.Location = "in the far corner of the room, on the same side as the door";
            item.IsDiscovered = true;

            // SMALL HANDLE
            item = FindItemByName("SMLHANDL");
            item.DisplayName = "Small Handle";
            item.Description = "A small handle that looks worn.";
            item.Location = "on the side";
            item.IsDiscovered = true;

        }

    }
}
