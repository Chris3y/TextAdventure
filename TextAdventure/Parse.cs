using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Parse
    {
        public Parse()
        {
        }

        public ActionResult Command(string command, Actor player)
        {
            var result = new ActionResult() { IsSuccessful = false, Outcome = "Unable to parse command" };

            string[] commandArray = command.Split(new char[] { ' ' }, 2);
            if (commandArray.Length == 2)
            {
                string verb = commandArray[0];
                string noun = commandArray[1];
                Entity entity;

                // INSPECT
                if (verb.Equals("inspect", StringComparison.CurrentCultureIgnoreCase))
                {
                    // GENERAL
                    entity = FindInteractiveThing(noun, player);
                    if (entity != null)
                    {
                        return entity.Inspect(player);
                    }

                    // PLAYER
                    if (noun.Equals("player", StringComparison.CurrentCultureIgnoreCase)
                        || noun.Equals("self", StringComparison.CurrentCultureIgnoreCase)
                        || noun.Equals("me", StringComparison.CurrentCultureIgnoreCase)
                        || noun.Equals("myself", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return player.Inspect(player);
                    }

                    // ROOM
                    if (noun.Equals("room", StringComparison.CurrentCultureIgnoreCase)
                        || noun.Equals("this room", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return player.CurrentRoom.Inspect(player);
                    }
                }
                
                // USE
                else if (verb.Equals("use", StringComparison.CurrentCultureIgnoreCase))
                {
                    entity = FindInteractiveThing(noun, player);
                    if (entity != null)
                    {
                        return entity.Use(player);
                    }
                }

                // OPEN
                else if (verb.Equals("open", StringComparison.CurrentCultureIgnoreCase))
                {
                    entity = FindInteractiveThing(noun, player);
                    if (entity != null)
                    {
                        return entity.Open(player);
                    }
                }

                // TAKE
                else if (verb.Equals("take", StringComparison.CurrentCultureIgnoreCase))
                {
                    entity = FindInteractiveThing(noun, player);
                    if (entity != null)
                    {
                        return entity.Take(player);
                    }
                }
            }
            
            return result;
            
        }
        
        public Entity FindInteractiveThing(string noun, Actor player)
        {
            Entity entity;

            // Search Room
            entity = player.CurrentRoom.Entities.Find(i => i.DisplayName.Equals(noun, StringComparison.CurrentCultureIgnoreCase));
            if (entity != null)
            {
                return entity;
            }

            // Then Player
            entity = player.Entities.Find(i => i.DisplayName.Equals(noun, StringComparison.CurrentCultureIgnoreCase));
            if (entity != null)
            {
                return entity;
            }

            return null;
        }
    }
}
