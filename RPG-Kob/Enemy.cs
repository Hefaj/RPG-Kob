using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Kob
{
    class Enemy : GameObject
    {
        private int exp;
        public char Type;

        public int Exp { get { return exp; } }
        
        static readonly string[] k =
        {@" <_\",
         @"    \\" ,
         @"      ＼(o_o )" ,
         @"        >　-\" ,
         @"      /    _＼" ,
         @"     /   /  ＼＼" ,
         @"    |   /     \_>" ,
         @"    /　/" ,
         @"   /　/|" ,
         @"  ( ( \" ,
         @"　|　|  ＼" ,
         @"　| / ＼  )" ,
         @"　| |  ) /" ,
         @" /  ) L_|" ,
         @"(_／" };
        static readonly int[] k_s = { 10, 10, 10, 10 };

        static readonly string[] w =
        {@"╚═(o.o)═╝" ,
        @"╚═(███)═╝" ,
        @"╚═(███)═╝" ,
        @" ╚═(███)═╝" ,
        @"  ╚═(███)═╝" ,
        @" ╚═(███)═╝" ,
        @" ╚═(███)═╝" ,
        @"  ╚═(███)═╝" ,
        @" ╚═(███)═╝" ,
        @"╚═(███)═╝" ,
        @"   ╚(███)╝" ,
        @" ╚═(███)═╝" ,
        @"  ╚(██)╝" ,
        @"   (█)" ,
        @"    *" };

        static readonly int[] w_s = { 10, 10, 10, 10 };

        static readonly string[] x =
        {@"╚═(o.o)═╝" ,
        @"╚═()═╝" ,
        @"╚═()═╝" ,
        @" ╚═()═╝" ,
        @"  ╚═()═╝" ,
        @" ╚═()═╝" ,
        @" ╚═()═╝" ,
        @"  ╚═()═╝" ,
        @" ╚═()═╝" ,
        @"╚═()═╝" ,
        @"   ╚()╝" ,
        @" ╚═()═╝" ,
        @"  ╚()╝" ,
        @"   ()" ,
        @"    *" };
        static readonly int[] x_s = { 10, 10, 10, 10 };

        static readonly string[] u =
           {@"      ░▄██▄░░░▄▄░░░░░",
            @"   ░░░█▀█▀█░░░░▀█▄░░░",
            @"    ░░██▄██░░░░░░▀█▄░░",
            @"    ░░░▀▄▀░░░▄▄▄▄▄▀▀░░",
            @" ░░░░▄▄▄██▀▀▀▀░░░░░░░",
            @" ░░░█▀▄▄▄█▀▀▀░░",
            @" ░░░█░▄▄▄██▀▀▀░░",
            @"░▄░█░░░▄▄▀█▀▀▀ ░░",
            @"░▀██░░░▄▀▀█▀▀▀ ░░",
            @" ░░░░░░░▄▄██▄▄░░░",
            @"    ░░░░▀███▀█░▄░░",
            @"     ░░░█▀▄▀▄▀█▄░░",
            @"    ░░░█▀░░░░░░░█░░",
            @"    ░░░█░░░░░░░░█░░",
            @"    ░░░█░░░░░░░░█░░",
        };
        static readonly int[] u_s = { 10, 10, 10, 10 };

        static readonly string[] h =
         {  @"     ▄▄▀▀▀██▀▀▀▀▀▀▀▄",
            @"  ▄▄▀░░░░░░░░░░░░░░░▀█",
            @"  █░░░░░▀▄░░▄▀░░░░░░░░█",
            @"   ██▄░░▀▄▀▀▄▀░░▄██▀░█",
            @"  █▀█░▀░░░▀▀░░░▀░█▀░░█",
            @"  █░░▀▀░░░░░░░░▀▀░░░░░█",
            @" █░░░░░░░░░░░░░░░░░░░█",
            @"  █░░▀▄░░░░▄▀░░░░░░░░█",
            @"  █░░░░░░░░░░░▄▄░░░░█",
            @"   █▀██▀▀▀▀██▀░░░░░░█",
            @"   █░░▀████▀░░░░░░░█",
            @"    █░░░░░░░░░░░░▄█",
            @"     ██░░░░░█▄▄▀▀░█",
            @"      ▀▀█▀▀▀▀░░░░░░█",
            @"      ▀▀█▀▀▀▀░░░░░░█"};
        static readonly int[] h_s = { 10, 10, 10, 10 };

        private readonly Dictionary<char, string> enemies_all;
        private readonly string[] model;

        public string[] Model { get { return model; } }
        public Enemy(char type, Point location) : base(location)
        {
            enemies_all = new Dictionary<char, string>
            { 
                { 'x', "Proces"},
                { 'w', "Alokator Płytowy"},
                { 'k', "K'Obs"},
                { 'u', "USOS"},
                { 'h', "???"},
            };

            this.Type = type;
            this.name = enemies_all[type];

            if (type == 'k')
            {
                this.model = k;
                this.SetStats(k_s);
                this.exp = 100;
            } 
            else if (type == 'w')
            {
                this.model = w;
                this.SetStats(w_s);
                this.exp = 100;
            }
            else if (type == 'u')
            {
                this.model = u;
                this.SetStats(u_s);
                this.exp = 100;
            }
            else if (type == 'h')
            {
                this.model = h;
                this.SetStats(h_s);
                this.exp = 100;
            }
            else
            {
                this.model = x;
                this.SetStats(x_s);
                this.exp = 100;
            }
                
        }

        public bool CheckLoc(Point p)
        {
            if (location.Equals(p)) return true;
            return false;
        }
    }
}
