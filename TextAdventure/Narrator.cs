using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Narrator
    {
        public Narrator(Timeline timeline)
        {
            timeline = Timeline;

        }
        public Timeline Timeline { get; set; }
        public List<string> OutputLog { get; set; } = new List<string>();

        public void Announce(string text = "", params string[] args)
        {
            string output = string.Format(text, args);
            Console.WriteLine(output);
            OutputLog.Add(output);
        }

        public void ListentTo(Entity entity)
        {
            entity.Inspected += Entity_InteractedWith;
            entity.Opened += Entity_InteractedWith;
            entity.Taken += Entity_InteractedWith;
            entity.Used += Entity_InteractedWith;
            entity.Notified += Entity_InteractedWith;
            entity.Entered += Entity_InteractedWith;
        }

        private void Entity_InteractedWith(object sender, EntityEventArgs e)
        {
            Announce(e.Result.Outcome);
        }
    }
}
