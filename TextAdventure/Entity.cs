using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAdventure
{
    public class Entity
    {
        public Entity(string name, Timeline timeline)
        {
            Name = name;
            Entities = new List<Entity>();
            InspectedCount = 0;
            TakenCount = 0;
            UsedCount = 0;
            OpenedCount = 0;
            Timeline = timeline;
            IsInspectable = true;
            AttachLocalEvents();
            AttachWorldEvents(timeline);
        }

        private bool isTakable;

        public string Name { get; set; }
        public Timeline Timeline { get; private set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public Room CurrentRoom { get; set; }
        public bool IsDiscovered { get; set; }
        public List<Entity> Entities { get; set; }
        public ThingTypes ThingType { get; set; }
        public Entity Owner { get; set; }

        public bool IsTakable
        {
            get
            {
                if (IsDiscovered)
                {
                    return isTakable;
                }
                else
                {
                    return false;
                }
            }
            set { isTakable = value; }
        }
        public bool IsEnterable { get; set; }
        public bool IsOpenable { get; set; }
        public bool IsUsable { get; set; }
        public bool IsInspectable { get; set; }

        public bool IsOpen { get; set; }
        public bool IsLocked { get; set; }

        public string NoInspectReason { get; set; }
        public string NoOpenReason { get; set; }
        public string NoTakeReason { get; set; }
        public string NoUseReason { get; set; }
        public string NoEnterReason { get; set; }

        public int InspectedCount { get; set; }
        public int TakenCount { get; set; }
        public int UsedCount { get; set; }
        public int OpenedCount { get; set; }

        protected string GetItemDescription()
        {
            var items = GetItems();
            StringBuilder sb = new StringBuilder();
            if (items.Count() > 0)
            {
                sb.Append(string.Format("There is a {0} {1}.", items[0].DisplayName, items[0].Location));
                for (int i = 1; i <= items.Count() - 1; i++)
                {
                    sb.Remove(sb.Length - 1, 1);
                    if (i != items.Count() - 1)
                    {
                        sb.Append(string.Format(" a {0} {1},", items[i].DisplayName, items[i].Location));
                    }
                    else
                    {
                        sb.Append(string.Format(" and a {0} {1},", items[i].DisplayName, items[i].Location));
                    }
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append(".");
            }
            else
            {
                sb.Clear();
            }
            return sb.ToString();
        }
        protected List<Item> GetItems()
        {
            var items = Entities.FindAll(i => i.ThingType == ThingTypes.Item && i.IsDiscovered).Cast<Item>().ToList();

            return items;
        }
        public virtual string GetFullDescription()
        {            
            if (IsDiscovered)
            {
                return string.Format("{0} {1}", Description, GetItemDescription());
            }

            return string.Empty;
        }

        protected ActionResult Notify(string message)
        {
            var result = new ActionResult();
            result.Outcome = message;
            result.IsSuccessful = true;
            OnNotified(result);
            return result;
        }
        public virtual ActionResult Inspect(Actor doer)
        {
            var result = new ActionResult();

            if (!IsInspectable)
            {
                result.IsSuccessful = false;
                result.Outcome = "You cannot inspect that.";

                OnInspected(result);
                return result;
            }
               
            Entities.ForEach(i => i.IsDiscovered = true);
            result.Outcome = GetFullDescription();
            OnInspected(result);
            InspectedCount += 1;

            return result;
        }     
        public virtual ActionResult Open(Actor doer)
        {
            var result = new ActionResult();

            if (!IsLocked)
            {
                IsOpen = true;
                result.IsSuccessful = true;
                result.Outcome = string.Format("{0} opened {1}", doer.DisplayName, this.DisplayName);
                OnOpened(result);
                OpenedCount++;

                return result;
                }
            else
            {
                result.IsSuccessful = false;
                result.Outcome = string.Format("You cannot open that. {0}", this.NoOpenReason);
                OnOpened(result);
                return result;
            }

        }
        public ActionResult Take(Actor doer)
        {
            var result = new ActionResult();

            if (IsTakable)
            {
                // if no current owner, add owner
                if (this.Owner == null)
                {                    
                    doer.Entities.Add(this);
                    this.Owner = doer;
                    result.IsSuccessful = true;
                    result.Outcome = string.Format("{0} has taken {1}.", doer.DisplayName, this.DisplayName);

                    OnTaken(result);
                    TakenCount++;

                    return result;
                }
                // if already owned, no change
                else if (this.Owner.Equals(doer))
                {
                    result.IsSuccessful = false;
                    result.Outcome = string.Format("{0} already owns {1}.", doer.DisplayName, this.DisplayName);

                    OnTaken(result);
                    return result;
                }
                // otherwise, change owner
                else
                {
                    this.Owner.Entities.Remove(this);
                    doer.Entities.Add(this);
                    this.Owner = doer;
                    result.IsSuccessful = true;
                    result.Outcome = string.Format("{0} has taken {1}.", doer.DisplayName, DisplayName);

                    OnTaken(result);
                    TakenCount++;

                    return result;
                }
            }
            else
            {
                result.IsSuccessful = false;
                result.Outcome = string.Format("{0} cannot be taken. {1}", DisplayName, NoTakeReason);

                OnTaken(result);
                return result;
            }


        }
        public virtual ActionResult Use(Actor doer)
        {
            var result = new ActionResult();

            if (IsUsable)
            {
                result.IsSuccessful = true;
                result.Outcome = string.Format("{0} used the {1}.", doer.DisplayName, DisplayName);                
                
                OnUsed(result);
                UsedCount++;

                return result;
            }
            else
            {
                result.IsSuccessful = false;
                result.Outcome = string.Format("You cannot use that. {0}", NoUseReason);

                OnUsed(result);
                return result;
            }
            
        }
        public virtual ActionResult Enter(Actor doer)
        {
            var result = new ActionResult();
            if (!IsEnterable)
            {
                result.IsSuccessful = false;
                result.Outcome = string.Format("You can't enter that. {0}", NoEnterReason);

                OnEntered(result);
                return result;
            }

            result.IsSuccessful = true;
            result.Outcome = string.Format("{0} entered {1}. {2}", doer.DisplayName, DisplayName, GetFullDescription());

            OnEntered(result);
            return result;
        }

        protected virtual void AttachLocalEvents()
        {            
        }
        protected virtual void AttachWorldEvents(Timeline timeline)
        {
        }

        public event EventHandler<EntityEventArgs> Notified;
        public event EventHandler<EntityEventArgs> Inspected;
        public event EventHandler<EntityEventArgs> Opened;
        public event EventHandler<EntityEventArgs> Used;
        public event EventHandler<EntityEventArgs> Taken;
        public event EventHandler<EntityEventArgs> Entered;
        
        protected virtual void OnNotified(ActionResult result)
        {
            EventHandler<EntityEventArgs> handler = Notified;
            if (handler != null)
            {
                handler(this, new EntityEventArgs(result));
            }
        }
        protected virtual void OnInspected(ActionResult result)
        {
            EventHandler<EntityEventArgs> handler = Inspected;
            if (handler != null)
            {
                handler(this, new EntityEventArgs(result));
            }
        }
        protected virtual void OnUsed(ActionResult result)
        {
            EventHandler<EntityEventArgs> handler = Used;
            if (handler != null)
            {
                handler(this, new EntityEventArgs(result));
            }
        }
        protected virtual void OnTaken(ActionResult result)
        {
            EventHandler<EntityEventArgs> handler = Taken;
            if (handler != null)
            {
                handler(this, new EntityEventArgs(result));
            }
        }
        protected virtual void OnOpened(ActionResult result)
        {
            EventHandler<EntityEventArgs> handler = Opened;
            if (handler != null)
            {
                handler(this, new EntityEventArgs(result));
            }
        }
        protected virtual void OnEntered(ActionResult result)
        {
            EventHandler<EntityEventArgs> handler = Entered;
            if (handler != null)
            {
                handler(this, new EntityEventArgs(result));
            }
        }

    }
}
