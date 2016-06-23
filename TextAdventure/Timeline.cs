using System;
using TextAdventure;

namespace TextAdventure
{
    public class Timeline 
    {
        public Timeline()
        {
        }

        public event EventHandler<EntityEventArgs> PlayerFirstInspected;
        public event EventHandler<EntityEventArgs> MicrochipFirstTaken;

        public void OnPlayerFirstInspected()
        {
            EventHandler<EntityEventArgs> handler = PlayerFirstInspected;
            if (handler != null)
            {
                var result = new ActionResult();
                result.IsSuccessful = true;
                handler(this, new EntityEventArgs(result));
            }
        }
        public void OnMicrochipFirstTaken()
        {
            EventHandler<EntityEventArgs> handler = MicrochipFirstTaken;
            if (handler != null)
            {
                var result = new ActionResult();
                result.IsSuccessful = true;
                handler(this, new EntityEventArgs(result));
            }
        }

        public void FireEvent(EventHandler<EntityEventArgs> eventToFire)
        {
            EventHandler<EntityEventArgs> handler = eventToFire;
            if (handler != null)
            {
                var result = new ActionResult();
                result.IsSuccessful = true;
                handler(this, new EntityEventArgs(result));
            }
        }
    }
}