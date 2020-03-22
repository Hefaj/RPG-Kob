using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace RPG_Kob
{
    class Song
    {

        
        // Pozwala na wielowatkowe odtwarzanie muzyczek, ale jakos to nie działa, problem z synchronizacją
        // Wydaje mi się, że Console.Beep jest operacją wymagającą operacji na chronionych danych (pewnie jakiś mutex)
        // Przez co niby wiecej niż jeden wątek, ale i tak jeden czeka na beepa wcześniejszego
        // Ale nic nie zmieniałem, wystarczy odpowiednio muzyke wpisac w pliku i bedzie dzialac na jednym
             

        private int[][] _song;
        private int len;
        private int _step;
        Thread[] Threads;

        public Song(List<int[]> song, int step)
        {
            len = song.Count;
            _song = new int[song.Count][];
            for (int i =0;i<song.Count;i++)
                _song[i] = song[i];
                _step = step;
        }

        public void Play()
        {
            Threads = new Thread[len];
            int itr = 0;

            foreach (var i in _song)
                Threads[itr++] = new Thread(_Play);

            itr = 0;

                foreach (var i in Threads)
                    i.Start(_song[itr++]);
        }

        private void _Play(object obj)
        {
            int[] i = (int[])obj;
            while (Program.music)
            {
                foreach (var _i in i)
                {
                    if (_i == -1)
                        System.Threading.Thread.Sleep(1000 / _step);
                    else
                    {
                        Console.Beep(_i, 1000 / _step);
                    }
                }
            } 
        }
    }

    class Music
    {
        private static float F(float i) => (int)(440 * Math.Pow(2, -i / 12));
        private Dictionary<string, float> f = new Dictionary<string, float>()
            {
                { "B7", -37},
                { "A#7", -36},
                { "A7", -35},
                { "G#7", -34},
                { "G7", -33},
                { "F#7", -32},
                { "F7", -31},
                { "E7", -30},
                { "D#7", -29},
                { "D7", -28},
                { "C#7", -27},
                { "C7", -26},

                { "B6", -25},
                { "A#6", -24},
                { "A6", -23},
                { "G#6", -22},
                { "G6", -21},
                { "F#6", -20},
                { "F6", -19},
                { "E6", -18},
                { "D#6", -17},
                { "D6", -16},
                { "C#6", -15},
                { "C6", -14},

                { "B5", -13},
                { "A#5", -12},
                { "A5", -11},
                { "G#5", -10},
                { "G5", -1},
                { "F#5", -9},
                { "F5", -8},
                { "E5", -7},
                { "D#5", -6},
                { "D5", -5},
                { "C#5", -4},
                { "C5", -3},

                { "B4", -2},
                { "A#4", -1},
                { "A4", 0},
                { "G#4", 1},
                { "G4", 2},
                { "F#4", 3},
                { "F4", 4},
                { "E4", 5},
                { "D#4", 6},
                { "D4", 7},
                { "C#4", 8},
                { "C4", 9},

                { "B3", 10},
                { "A#3", 11},
                { "A3", 12},
                { "G#3", 13},
                { "G3", 14},
                { "F#3", 15},
                { "F3", 16},
                { "E3", 17},
                { "D#3", 18},
                { "D3", 19},
                { "C#3", 20},
                { "C3", 21},

                { "B2", 22},
                { "A#2", 23},
                { "A2", 24},
                { "G#2", 25},
                { "G2", 26},
                { "F#2", 27},
                { "F2", 28},
                { "E2", 29},
                { "D#2", 30},
                { "D2", 31},
                { "C#2", 32},
                { "C2", 33}
            };
        private Dictionary<string, Song> songs = new Dictionary<string, Song>();

        private string[] lines, nutes;

        private List<int[]> converted = new List<int[]>();

        private string nameSong, tactSong;

        public Music()
        {
            int iter;

            f = f.ToDictionary(kvp => kvp.Key, kvp => F(kvp.Value));
            f.Add("#", -1);

            string[] dirs = Directory.GetFiles(@"C:\Users\hefaj\source\repos\RPG-Kob\RPG-Kob\music", "song_*");

            foreach (string dir in dirs)
            {
                iter = 0;
                try
                {
                    lines = File.ReadAllLines(dir);
                    
                    foreach (string _line in lines)
                    {
                        string line = _line.Replace(" ", string.Empty);
                        //Console.WriteLine(line);
                        if (line.Contains("[name]="))
                            nameSong = line.Split(new string[] { "[name]=" }, StringSplitOptions.RemoveEmptyEntries)[0];             
                        else if (line.Contains("[tact]="))
                            tactSong = line.Split(new string[] { "[tact]=" }, StringSplitOptions.RemoveEmptyEntries)[0];
                        else
                        {
                            
                            nutes = line.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                            converted.Add(new int[nutes.Length]);

                            for (int i = 0; i < nutes.Length; i++)
                                converted[iter][i] = (int)f[nutes[i]];
                            iter++;
                        }
                    }

                    // add song
                    songs.Add(nameSong, new Song(converted, int.Parse(tactSong)));


                }
                catch (IOException e)
                {
                    Console.WriteLine("Nie moge otworzyc pliku z muzyką :(");
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void PlaySong(string str)
        {
            songs[str].Play();
        }
    }
}