using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Kob
{
    class Item
    {
        private string name;
        private Point location;
        private bool only_Combat;

        private string descript;

        public string Name { get { return name; } }
        public string Desc { get { return descript; } }

        public bool Only_Combat { get { return only_Combat; } }

        //public static Dict
        private Dictionary<char, string> items_all;
        public Item(char type, Point location)
        {
            // readItemsFromFile();
            items_all = new Dictionary<char, string>
            {
                { 'a', "Ambicja"},
                { 'c', "Wkuwanie"},
            };

            this.name = items_all[type];
            this.location = location;

            if (type == 'a')
            {
                only_Combat = true;
                this.descript = "Nic ci nie pomoże.";
            }
            else if (type == 'c')
            {
                only_Combat = false;
                this.descript = "Przywraca 50 hp.";
            } 
            else
            {
                only_Combat = false;
                this.descript = "???";
            }    
        }

        public bool CheckLoc(Point p)
        {
            if (location.Equals(p)) return true;
            return false;
        }

        internal bool Used(Player p, bool combat)
        {
            if (this.Name == "Wkuwanie")
            {
                p.Healing(50);
                return true;
            } 
            else if (this.Name == "Semafor" && only_Combat == combat)
            {
                return true;
            }
            return false;
        }
    }
}
