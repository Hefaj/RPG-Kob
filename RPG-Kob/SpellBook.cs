using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Kob
{
    public class Spell
    {
        public string Name { get; private set; }
        public string Desc { get; private set; }
        public int Type { get; private set; }
        public int ManaCost { get; private set; }
        public int UnlockLevel { get; private set; }

        public Spell(string name, string desc, int type, int manaCost, int unlock)
        {
            Name = name;
            Desc = desc;
            Type = type;
            ManaCost = manaCost;
            UnlockLevel = unlock;
        }
    }

    public class SpellBook
    {
        public List<Spell> allSpell;
        public List<Spell> activeSpell = new List<Spell>();

        public SpellBook()
        {
            // dodane recznie, nie widze potrzeby pobierania z pliku
            // lvl  odblokowanie skili 
            // int  efektywnosc ich dzialania

            // [nazwa];[typ];[koszt many];[desc]
            // nazwa - Wyświetlana nazwa skilla
            // typ - rodzaj zaklecia:
            //      0 - przywraca hp
            //      1 - zwieksza szanse na kryta
            //      2 - zwieksza szanse na unik
            //      3 - ???

            allSpell = new List<Spell>() {
                new Spell("Odsapnij", "Pozwala na przywrocenie hp.", 0, 40, 1),
                //new Spell("Użyj telefonu", "Zwieksza tymczasowo siłe", 1, 30, 2),
                //new Spell("Opanowanie nerwowych ruchów", "Zwieksza tymczasowo zręczność", 2, 30, 2)
            };
            LevelUp(1);
        }

        public void LevelUp(int lvl)
        {
            // odblokowanie nowego skilla

            foreach (var spell in allSpell)
            {
                if (spell.UnlockLevel == lvl)
                    activeSpell.Add(spell);
            }
        }

        public void Print()
        {
            int idx = 1;

            Console.WriteLine("Zdolności:");

            foreach (Spell i in activeSpell)
            {
                Console.WriteLine(idx++ + ". " + i.Name);
                Console.WriteLine(i.Desc);
            }
        }

        public int ManaCost(int i) { return activeSpell[i].ManaCost; }
        public int GetType(int i) { return activeSpell[i].Type; }
        internal bool CanCast(int i, int mana)
        {
            if (activeSpell[i].ManaCost <= mana) 
                return true;
            return false;
        }
    }
}
