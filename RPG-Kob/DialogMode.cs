using System;
using System.Threading;

namespace RPG_Kob
{
    class DialogMode
    {
        //public DialogMode(){ }

        public void Print(int idx)
        {
            string dialog = FindDialog(idx);
            Console.Clear();
            for (int i = 0; i < dialog.Length; i++)
            {
                Console.Write(dialog[i]);
                Thread.Sleep(30);
            }

            Console.WriteLine("\nDalej");
            Console.ReadKey();
            //Console.Clear();
        }

        private string FindDialog(int idx)
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\hefaj\source\repos\RPG-Kob\RPG-Kob\dialog.txt");
            return lines[idx];
        }
    }
}
