using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class ActionResult
    {
        public string Outcome { get; set; }
        public bool IsSuccessful { get; set; }
        public bool IsCancelled { get; set; }
    }
}
