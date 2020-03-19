using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Kob
{
    class GameObject
    {
        protected string name;
        protected Point location;

        protected int _str;
        protected int _agi;
        protected int _vit;
        protected int _int;

        protected int lvl;

        protected int hp;

        private Random r;

        private int critChance;
        private int dodgeChance; 

        public string Name { get { return name; } }
        public int S { get { return _str; } }
        public int A { get { return _agi; } }
        public int V { get { return _vit; } }
        public int I { get { return _int; } }

        public int Hp { get { return hp; } }

        public int Crit { get { return critChance; } }
        public int Dodge { get { return dodgeChance; } }
        public GameObject(Point location)
        {
            this.location = location;
            r = new Random();
        }

        protected void SetStats(int[] s)
        {
            _str = s[0];
            _agi = s[1];
            _vit = s[2];
            _int = s[3];
            lvl = 0;
            hp = _vit * 10;
            critChance = _agi / 3;
            dodgeChance = _agi / 4;
        }

        public void Show() // del
        {
            Console.WriteLine("nazwa: " + name + ", lokacja: " + location);
        }

        internal bool GetDmg(int dmg)
        {
            if (!Is_Dodge())
            {
                this.hp -= dmg;
                if (Is_Dead()) return true;
            }
            return false;
        }

        internal int Attack()
        {
            if (Is_Crit()) return _str * 2;
            else return _str;
        }

        internal bool Is_Dead()
        {
            if (this.hp <=0) return true;
            return false;
        }

        private bool Is_Crit()
        {
            if(r.Next(1, 100 / critChance) == 1) return true;
            return false;
        }

        private bool Is_Dodge()
        {
            if (r.Next(1, 100 / dodgeChance) == 1) return true;
            return false;
        }
    }
}
