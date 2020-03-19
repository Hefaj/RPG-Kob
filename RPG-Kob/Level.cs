using System;
using System.Collections.Generic;
using System.IO;


namespace RPG_Kob
{
    class Level
    {
        private const int width = 20, height = 10;
        private char[,] map_array = new char[width, height];
        private Point player_loc, next_level_pos, back_level_pos = null;

        char[] enemies = {'x','k','w','u','h'};
        char[] items = { 'a', 'c' };

        private List<Enemy> enemies_list = new List<Enemy>();
        private List<Item> items_list = new List<Item>();
        
        public Point Player_loc { get { return player_loc; } }

        public Level(string name)
        {
            ReadFromFile(name);
        }

        private void ReadFromFile(string name)
        {
            string[] lines = System.IO.File.ReadAllLines(name);

            int x, y = 0;
            foreach (string line in lines)
            {
                x = 0;
                foreach (char l in line)
                {
                    map_array[x, y] = l;
                    if (l == 'o') player_loc = new Point(x, y);
                    else if (l == 'z')
                    {
                        next_level_pos = new Point(x, y);
                        map_array[x, y] = ' ';
                    }
                    else if (l == 'b')
                    {
                        back_level_pos = new Point(x, y);
                        map_array[x, y] = ' ';
                    }
                    else if(Is_enemy(l))
                    {
                        Enemy en = new Enemy(l, new Point(x, y));
                        enemies_list.Add(en);
                    }
                    else if (Is_item(l))
                    {
                        Item en = new Item(l, new Point(x, y));
                        items_list.Add(en);
                    }

                    x++;
                }
                y++;
            }
        }

        private bool Is_enemy(char l)
        {
            foreach (char c in enemies)
                if(c.Equals(l)) return true;
            return false;
        }

        private bool Is_item(char l)
        {
            foreach (char c in items)
                if (c.Equals(l)) return true;
            return false;
        }

        public void ShowMap()
        {
            for (uint y = 0; y < height; y++)
            {
                for (uint x = 0; x < width; x++)
                    Console.Write(map_array[x, y]);
                Console.Write("\n");
            }
        }

        internal int Player_Move(Point move, Player p)
        {
            Point w = new Point(player_loc + move);
            // sprawdzanie czy nie wychodzi poza mape (błąd mapy, jakaś luka do naprawy)
            if (w.CheckPoint(width, height)) return 1;
            // sprawdzanie czy nie wchodzi w siane
            else if (map_array[w.X, w.Y] == '#') return 2;
            // sprawdzanie czy nie wchodzi w przedmiot
            else if (Is_item(map_array[w.X, w.Y]))
            {
                map_array[w.X, w.Y] = ' ';
                return 3;
            }
            // sprawdzanie czy nie wchodzi w przeciwnika
            else if (Is_enemy(map_array[w.X, w.Y]))
            {
                map_array[w.X, w.Y] = ' ';
                return 4;
            }
            // sprawdzanie czy nie wchodzi do nastepnego levela
            else if (next_level_pos.Equals(w)) return 5;
            // sprawdzanie czy nie wchodzi do poprzedniego levela
            else if (!(back_level_pos is null) && back_level_pos.Equals(w)) return 6;
            // sprawdzanie czy skonczono gre
            else if (map_array[w.X, w.Y] == '|') return 7;
            // poruszanie
            else
            {
                map_array[player_loc.X, player_loc.Y] = ' ';
                player_loc = w;
                map_array[player_loc.X, player_loc.Y] = 'o';
                p.NewLoc(player_loc);
                return 0;
            }
        }

        public Enemy FindEnemy(Point loc)
        {
            foreach (Enemy e in enemies_list)
                if (e.CheckLoc(loc)) return e;
            return null;
        }

        public Item Find_Item(Point loc)
        {
            foreach (Item e in items_list)
                if (e.CheckLoc(loc)) return e;
            return null;
        }
        
    }
}
