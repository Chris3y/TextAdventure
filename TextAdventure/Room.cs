using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Room : Entity
    {
        public Room(string name, Timeline timeline) : base(name, timeline)
        {
            Name = name;
            ThingType = ThingTypes.Room;
            IsEnterable = true;
        }
         
        public override string GetFullDescription()
        {
            return string.Format("{0} {1}", this.Description, this.GetExitDescription());
        }
        List<Exit> GetExits()
        {
            var exits = Entities.FindAll(i => i.ThingType == ThingTypes.Exit && i.IsDiscovered).Cast<Exit>().ToList();

            return exits;
        }
        string GetExitDescription()
        {
            var exits = GetExits();
            StringBuilder sb = new StringBuilder();
            if (exits.Count() > 0)
            {
                // start with first item
                sb.Append(string.Format("{0}, there is a {1}.", exits[0].GetLocation(this), exits[0].DisplayName));
                // grab first char
                char firstChar = sb[0];
                // remove first char from string
                sb.Remove(0, 1);
                // reinsert char as capitalised to start new sentance
                sb.Insert(0, firstChar.ToString().ToUpper());
                // append where it leads
                if (GetJoiningRoom(exits[0]).IsDiscovered)
                {
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(string.Format(", which leads to the {0}.", GetJoiningRoom(exits[0]).DisplayName));
                }
                // process any remaining items
                for (int i = 1; i <= exits.Count() -1; i++)
                {
                    sb.Remove(sb.Length - 1, 1);
                    sb.Append(",");
                    if (i != exits.Count()-1)
                    {
                        sb.Append(string.Format(" a {0} {1},", exits[i].DisplayName, exits[0].GetLocation(this)));
                        if (GetJoiningRoom(exits[i]).IsDiscovered)
                        {
                            sb.Append(string.Format(" which leads to the {0},", GetJoiningRoom(exits[i]).DisplayName));
                        }
                    }
                    else
                    {
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append(string.Format(" and there is a {0} {1}.", exits[i].DisplayName, exits[0].GetLocation(this)));
                        if (GetJoiningRoom(exits[i]).IsDiscovered)
                        {
                            sb.Remove(sb.Length - 1, 1);
                            sb.Append(string.Format(", which leads to the {0}.", GetJoiningRoom(exits[i]).DisplayName));
                        }
                    }

                }
            }
            else
            {
                sb.Clear();
                sb.Append("There are no visible exits.");
            }
            return sb.ToString();
        }
        public Room GetJoiningRoom(Exit exit)
        {
            if (exit.Room1.Equals(this))
            {
                return exit.Room2;
            }
            else
            {
                return exit.Room1;
            }
        }


    }
}
