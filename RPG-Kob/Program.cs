using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using static System.Console;

namespace RPG_Kob
{
    public class Program
    {
        public static bool music = true;

        static void Main()
        {
            Console.CursorVisible = false;

            DialogMode dialog = new DialogMode();
            List<Level> Levels = new List<Level>();
            int map_level = 0;
            int info_log = 0;

            bool combat = false;

            // Wczytanie muzyczek z pliku
            Music _music = new Music();
            _music.PlaySong("TravelMusic");

            // inicjacja poziomow
            try
            {
                // szukanie wszystkich map
                string[] dirs = Directory.GetFiles(@"C:\Users\hefaj\source\repos\RPG-Kob\RPG-Kob\maps", "mapa_*");
                //Console.WriteLine("Liczba wczytanych map {0}.", dirs.Length);

                // ładowanie do pamieci wszystkich map
                foreach (string dir in dirs)
                {
                    //Console.WriteLine("level");
                    Levels.Add(new Level(dir));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Błąd: {0}", e.ToString());
                Environment.Exit(0);
            }
            
            if (Levels.Count == 0) Environment.Exit(0);

            // okno wyboru klasy
            ChoiceClass cc = new ChoiceClass();
            Stats s = cc.Select();
            cc = null;

            // inicjacja gracza
            Player gracz = new Player("Student", Levels[map_level].Player_loc, s);

            //dialog.Print(0);
            //dialog.Print(1);
            //dialog.Print(2);

            Console.Clear();

            //glowna petla
            while (true)
            {
                // wyswiwtlanie aktywnej mapy
                Levels[map_level].ShowMap();
                // print info log
                Show_info_log(info_log);
                // print dostepne akcje
                Show_all_action();

                // wybor akcji
                //string c = Console.ReadKey();
                ConsoleKeyInfo c = Console.ReadKey();
                switch (c.Key)
                {
                    case ConsoleKey.W:
                        info_log = Levels[map_level].Player_Move(new Point(0,-1), gracz);
                        break;
                    case ConsoleKey.S:
                        info_log = Levels[map_level].Player_Move(new Point(0, 1), gracz);
                        break;
                    case ConsoleKey.A:
                        info_log = Levels[map_level].Player_Move(new Point(-1, 0), gracz);
                        break;
                    case ConsoleKey.D:
                        info_log = Levels[map_level].Player_Move(new Point(1, 0), gracz);
                        break;
                    case ConsoleKey.I:
                        while (true)
                        {
                            Console.Clear();
                            gracz.Stats();
                            info_log = gracz.Inventory();

                            Console.WriteLine("(x) Wyłącz plecak");
                            string o = Console.ReadLine();
                            if (o == "x") break;

                            int i = 0;
                            if (int.TryParse(o, out i))
                                gracz.Use_Item(o, combat);
                        }

                        break;
                    case ConsoleKey.P:
                        gracz.Get_Exp(30);
                        break;
                    case ConsoleKey.M:


                        if (music)
                            music = !music;
                        else
                        {
                            //System.Threading.Tasks.Task.Run((Action)PlaySong);
                            music = !music;
                            _music.PlaySong("TravelMusic");
                        }
                        break;                   
                    default:
                        {
                            info_log = 0;
                            break;
                        }     
                }

                if (info_log == 1) Environment.Exit(0);
                //info_log == 2 nic nie robi
                else if (info_log == 3) 
                {
                    Dictionary<ConsoleKey, Point> loc = new Dictionary<ConsoleKey, Point>
                    {
                        { ConsoleKey.W, new Point(0,-1) },
                        { ConsoleKey.S, new Point(0, 1) },
                        { ConsoleKey.A, new Point(-1,0) },
                        { ConsoleKey.D, new Point(1, 0) }
                    };
                    /*dodanie itemu graczowi*/
                // pobranie itemu
                Item item = Levels[map_level].Find_Item(gracz.Get_Loc+loc[c.Key]);
                    //Console.WriteLine(item.Name);
                    gracz.Add_Item(item);
                }
                else if (info_log == 4)
                {
                    /*bitka*/
                    combat = true;
                    FightMode _arena = new FightMode(gracz, c.Key, Levels[map_level]);
                    combat = false;
                }
                else if (info_log == 5) map_level++;
                else if (info_log == 6) map_level--;
                else if (info_log == 7) Game_Over();
                Console.Clear();
            }    
        }

        private static void Game_Over()
        {
            string[] N = {
                "",
                "",
                "",
                "",
                "   *****************",
                "   *               *",
                "   *    KONIEC     *",
                "   *               *",
                "   *****************",
                "",
                "",
                "",
                "",
                "",
                "",

                @"    %%%",
                @"   =====",
                @"  &%&%%%&",
                @"  %& < <%",
                @"   &\__/",
                @"    \ |____",
                @"   .', ,  ()",
                @"  / -.  _)|",
                @" | _(_.    |",
                @" '-'\  ) |",
                @" mrf )    |",
                @"    /  .  ).",
                @"   / _. |",
                @" / '---':.-'|",
                @" (__.' /    /",
                @" \   ( /  /",
                @"   \ / _ |",
                @"    \  | '|",
                @"    | . \  | ",
                @"    | (     |",
                @"    |  \ \ |",
                @"     \  )\ |",
                @"    __)/ / \",
                @"----(_.Ooo'----",

                "",
                "",

                "Programista: Rafał Wójcik",
                "",
                "Grafik: Rafał Wójcik",
                "",
                "Dźwięk: Rafał Wójcik",
                "",
                "Fabuła: Rafał Wójcik",
                "",
                "Gra powstała na zaliczenie labolatoriów."
            };

            Array.Reverse(N); 

            string[] M = new string[14];

            for (int i = 0; i < M.Length; i++)
                M[i] = "";

            Console.Clear();

            for (int i = 0; i < N.Length; i++)
            {
                Console.Clear();

                foreach (string l in M)
                    Console.WriteLine(l);


                for (int j = M.Length-1; j > 0; j--)
                    M[j] = M[j-1];
                M[0] = N[i];

                System.Threading.Thread.Sleep(500);
                
            }
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void Show_info_log(int info_log)
        {
            string info_text;

            if (info_log == 1) info_text = "Wyjdzie poza mape, cos nie tak, jakaś luka!";
            else if (info_log == 2) info_text = "Uwaga ściana!";
            else if (info_log == 3) info_text = "Udało ci się coś znaleźć!";
            else if (info_log == 4) info_text = "Przeciwnik!";
            else if (info_log == 5) info_text = "Przejscie!";
            else info_text = "";

            Console.Write("\n");
            string error = (info_text.Length>0)?"# " + info_text + " #":"";
            for (int i = 0; i < error.Length; i++) Console.Write("#");
            Console.WriteLine("\n" + error);
            for (int i = 0; i < error.Length; i++) Console.Write("#");
            Console.Write("\n");
        }

        private static void Show_all_action()
        {
            Console.WriteLine("\nSterowanie: w (góra), s (dół), a (lewa), d (prawa)\n" +
                "Pozostałe: \ni (plecak)\nm ("+ ((music)? "Wyłącz": "Włącz") + " dźwięk) [musisz poczekać aż zagra do końca]");
        }
    }
}
