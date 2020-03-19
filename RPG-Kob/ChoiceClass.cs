using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Kob
{

    public class Stats
    {
        private readonly string name;
        private readonly string desc;
        private readonly int[] stat;
        
        public int[] Stat { get => stat; }


        public Stats(string n, int str, int agi, int vit, int knw, string d)
        {
            stat = new int[4];
            name = n;
            desc = d;
            stat[0] = str;
            stat[1] = agi;
            stat[2] = vit;
            stat[3] = knw;
        }

        public override string ToString() => $"{name}\n{desc}";
    }

    public class ChoiceClass
    {
        private readonly List<Stats> allClass = new List<Stats>();

        public ChoiceClass()
        {
            LoadStatsFromFile();
        }

        public Stats Select()
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Wybierz klase:");

                for (int i = 0; i < allClass.Count; i++)
                    Console.WriteLine(i + 1 + ". " + allClass[i].ToString());
                

                string s = Console.ReadLine();
                int _s = int.Parse(s);

                if (_s > 0  && _s <= allClass.Count)
                    return allClass[_s-1];
            }
        }

        void LoadStatsFromFile()
        {
            Stats s;
            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"C:\Users\hefaj\source\repos\RPG-Kob\RPG-Kob\settings\stats.txt");

                foreach (var line in lines)
                {
                    string[] st = line.Split(';');
                    s = new Stats(st[0], int.Parse(st[1]), int.Parse(st[2]), int.Parse(st[3]), int.Parse(st[4]), st[5]);
                    allClass.Add(s);
                }
            }
            catch (Exception)
            {

                Console.WriteLine("Błąd w pliku stats.");
                Console.ReadKey();
                Environment.Exit(0);
            }
            
        }
    }
}
