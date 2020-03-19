using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

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

        static Dictionary<char, string> enemies_all = new Dictionary<char, string>();
        static Dictionary<char, string[]> models = new Dictionary<char, string[]>();
        static Dictionary<char, int[]> stats = new Dictionary<char, int[]>();
        private readonly string[] model;

        public string[] Model { get { return model; } }


        //****
        static List<string> forModel = new List<string>();

        static Enemy()
        {
            ReadFromFileAllMobs();
        }

        private static void ReadFromFileAllMobs()
        {
            try
            {
                string[] dirs = Directory.GetFiles(@"C:\Users\hefaj\source\repos\RPG-Kob\RPG-Kob\mobs", "mob_*");
                string[] alias = new string[6];
                if (dirs.Length == default(int)) throw new Exception("Nie znaleziono zadnego pliku z mobami [pliki w pliku mobs o nazwie mob_[].txt ]");

                foreach (var path in dirs)
                {
                    string[] lines = File.ReadAllLines(path);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (i == default(int))
                        {
                            alias = lines[i].Split(';');
                            enemies_all.Add(alias[0][0], alias[1]);
                            stats.Add(alias[0][0], new int[] { int.Parse(alias[2]), int.Parse(alias[3]), int.Parse(alias[4]), int.Parse(alias[5]) });
                        }
                        else
                            forModel.Add(lines[i]);
                    }
                    models.Add(alias[0][0], forModel.ToArray());
                    forModel.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        // *****

        public Enemy(char type, Point location) : base(location)
        {
            
            this.Type = type;
            this.name = enemies_all[type];

            this.model = models[type];
            this.SetStats(stats[type]);

            //this.exp = 100;   
        }

        public bool CheckLoc(Point p)
        {
            if (location.Equals(p)) return true;
            return false;
        }
    }
}
