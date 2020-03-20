using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Kob
{
    class FightMode
    {
        public FightMode(Player p, ConsoleKey w, Level l)
        {
            Dictionary<ConsoleKey, Point> loc = new Dictionary<ConsoleKey, Point>
            {
                { ConsoleKey.W, new Point(0,-1) },
                { ConsoleKey.S, new Point(0, 1) },
                { ConsoleKey.A, new Point(-1,0) },
                { ConsoleKey.D, new Point(1, 0) }
            };

            Enemy enemy = l.FindEnemy(p.Get_Loc + loc[w]);


            if (Enemy._first_time_enemy[enemy.Type][0] == 0)
            {
                DialogMode dialog = new DialogMode();
                Enemy._first_time_enemy[enemy.Type][0]=-1;

                foreach(int i in Enemy._first_time_enemy[enemy.Type])
                    if (i != -1) dialog.Print(i);
                //Console.ReadKey();
            }

            if (!(enemy is null)) Fight(p, enemy);
            p.Get_Exp(enemy.Exp);
        }

        private void Fight(Player p, Enemy e)
        {

            while (true)
            {
                Console.Clear();
                Show_Area(p,e);

                bool END = false, action = false;

                string c = Console.ReadLine();

                switch (c)
                {
                    case "1":
                        END = e.GetDmg(p.Attack());
                        action = true;
                        break;
                    case "2":
                        break;
                    case "3":
                        while (true)
                        {
                            Console.Clear();
                            p.Stats();
                            p.Inventory();
                            Console.WriteLine("(x) Wyłącz plecak");
                            string o = Console.ReadLine();
                            if (o == "x") break;
                            p.Use_Item(o);
                            action = true;
                        }
                        
                        break;
                    default:
                        break;
                }
                if (END) break;

                if (action) p.GetDmg(e.Attack());

                if (p.Is_Dead()) End_Game();
            }
        }

        private void End_Game()
        {
            Console.Clear();
            Console.WriteLine("Nie zdałeś!");
            Environment.Exit(0);
        }

        private void Show_Area(Player p, Enemy e)
        {
            string[] player_model = p.Model;
            string[] enemy_model = e.Model;

            // print enemy stats
            Console.WriteLine("Siła: " + e.S +
                              " Zręczność: " + e.A +
                              " Witalność: " + e.V +
                              " Wiedza: " + e.I +
                              " Szansa na Kryta: " + e.Crit + "%" +
                              " Szansa na Unik: " + e.Dodge + "%");
            Console.WriteLine("Życie: " + e.Hp);
            Console.WriteLine(e.Name);
            // print models
            for (int i = 0; i < player_model.Length; i++)
                Console.WriteLine(player_model[i] + enemy_model[i]);
            // print player stats
            Console.WriteLine("Siła: " + p.S +
                             " Zręczność: " + p.A +
                             " Witalność: " + p.V +
                             " Wiedza: " + p.I +
                             " Szansa na Kryta: " + p.Crit + "%" +
                             " Szansa na Unik: " + p.Dodge + "%");
            Console.WriteLine("Życie: " + p.Hp);
            //action
            Console.WriteLine("(1) Atak, (2) Ksiązka, (3) Plecak");

        }
    }
}
