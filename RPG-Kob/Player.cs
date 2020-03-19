using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Kob
{
    class Player : GameObject
    {
        private List<Item> items_list = new List<Item>();

        private readonly string[] model = 
        {@"              " ,
         @"              " ,
         @"              " ,
         @"              " ,
         @"              " ,
         @"              " ,
         @"              " ,
         @"              " ,
         @"      ╔══╗    " ,
         @"   ¸.¸╚╗╔╝    " ,
         @" (¯`v´¯)╚╗    " ,
         @"[]`.¸. ══╝    " ,
         @"              " ,
         @"              " ,
         @"              " };
        private int[] stats;

        private int exp;
        private int exp_need_to_level;
        private int hp_max;
        private int exp_point;

        public string[] Model { get { return model; } } 

        public Player(string name, Point location, Stats s) : base(location)
        {
            stats = s.Stat;

            this.name = name;
            this.SetStats(stats);
            exp = 0;
            exp_need_to_level = 100;
            hp_max = 100;
            exp_point = 0;
        }

        internal void NewLoc(Point p)
        {
            location = p;
        }

        public Point Get_Loc
        {
            get { return location; }
        }

        public void Add_Item(Item i)
        {
            this.items_list.Add(i);
            Console.WriteLine("Dodano do plecaka: " + i.Name);
        }

        internal int Inventory()
        {
            int idx = 1;

            Console.WriteLine("Plecak:");

            foreach (Item i in items_list)
            {
                Console.WriteLine(idx++ + ". " + i.Name);
                Console.WriteLine(i.Desc);
            }
            return 0;
        }

        internal void Stats()
        {

            Console.WriteLine("Siła: " + S +
                             "\nZręczność: " + A +
                             "\nWitalność: " + V +
                             "\nWiedza: " + I +
                             "\nSzansa na Kryta: " + Crit + "%" +
                             "\nSzansa na Unik: " + Dodge + "%" +
                             "\nŻycie: " + Hp);
        }

        internal void Use_Item(string o, bool combat=true)
        {
            int i = int.Parse(o)-1;

            if (!(i<0 || i >= items_list.Count))
            {
                if (items_list[i].Used(this, combat))
                    items_list.RemoveAt(i);
            }
        }

        internal void Healing(int p)
        {
            this.hp += p;
        }

        public void Get_Exp(int exp)
        {
            this.exp += exp;
            Check_Level_Up();
        }

        private void Check_Level_Up()
        {
            if (exp >= exp_need_to_level)
                Level_Up();
        }

        private void Level_Up()
        {
            hp_max += 10;
            hp = hp_max;
            exp_point = 2;
            Console.Clear();

            while (exp_point >= 1)
            {
                Console.WriteLine("Wbiłeś poziom!\nMaksymalne życie podniesione o 10!\nRozdaj punkty.");
                Console.WriteLine("(1) Siła: {0}\n(2) Zręczność: {1}\n(3) Wiedza: {2}\n(4) Witalność: {3}",S,A,I,V);

                Console.WriteLine("Dostępne Punkty: " + exp_point);
                exp_point--;
                string _c = Console.ReadLine();

                switch (_c)
                {
                    case "1":
                        this._str++;
                        break;
                    case "2":
                        this._agi++;
                        break;
                    case "3":
                        this._int++;
                        break;
                    case "4":
                        this._vit++;
                        break;
                    default:
                        exp_point++;
                        break;
                }
                Console.Clear();
            }
           


        }
    }
}
