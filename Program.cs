using System;
using System.Collections.Generic;
using System.Linq;

namespace D_D
{
    internal class Program
    {
        private enum Genders
        {
            Male,
            Female
        }
        private static List<string> maleNames = new List<string>
        {
            "Aldrik", "Belizar", "Velian", "Galion", "Daerion",
            "Eldan", "Zhelian", "Zefirian", "Invir", "Kaiden",
            "Lorian", "Malfion", "Norian", "Orfelian", "Pelagius",
            "Reinar", "Sivarius", "Talion", "Fariel", "Celian"
        };
        private static List<string> femaleNames = new List<string>
        {
            "Nymphea", "Brianna", "Veliandra", "Glimmera", "Daelia",
            "Elvira", "Zheliana", "Zefira", "Ilmariel", "Callista",
            "Liriel", "Meliandra", "Lysandra", "Oriana", "Periandra",
            "Ravenna", "Silindra", "Talianna", "Firael", "Celiandra"
        };
        private enum Classes
        {
            Bard,
            Fighter,
            Barbarian,
            Druid,
            Sorcerer,
            Cleric,
            Wizard,
            Monk,
            Paladin,
            Rogue,
            Ranger,
            Warlock
        }
        private enum Races
        {
            Human,
            Gnome,
            Dwarf,
            Elf,
            HalfElf,
            Halfling,
            HalfOrc,
            Tiefling,
            Dragonborn
        }

        private static Random rnd = new Random();
        
        static private string getRandomName(Genders selectedGender)
        {
            if (selectedGender == 0)
            {
                return maleNames[rnd.Next(maleNames.Count)];
            }
            else
                return femaleNames[rnd.Next(femaleNames.Count)];
        }
        class Character
        {
            public string name;
            public Genders gender;
            public Races chrRace;
            public Classes chrClass;
            public uint level = 1;
            public int skillBonus = 2;

            public string bonusRace;
            public Dictionary<string, int> chrStats = new Dictionary<string, int>();
            List<int> NumsForCharts = new List<int> { 15, 14, 13, 12, 10, 8 };
            public int PullRandomStat()
            {
                int rndIndex = rnd.Next(NumsForCharts.Count);
                int rndStat = NumsForCharts[rndIndex];
                NumsForCharts.RemoveAt(rndIndex);
                return rndStat;
            }
            public void ImprovingAbilities(Races race) 
            {
                switch (race)
                {
                    case Races.Human:
                        foreach (string statName in chrStats.Keys.ToList())
                        {
                            chrStats[statName] += 1;
                        }
                        bonusRace = "All Stats +1";
                        break;
                    case Races.Gnome:
                        chrStats["Intelligence"] += 2;
                        bonusRace = "Intelligence +2";
                        break;
                    case Races.Dwarf:
                        chrStats["Constitution"] += 2;
                        bonusRace = "Constitution +2";
                        break;
                    case Races.Elf:
                        chrStats["Dexterity"] += 2;
                        bonusRace = "Dexterity +2";
                        break;
                    case Races.HalfElf:
                        chrStats["Charisma"] += 2;
                        bonusRace = $"Charisma +2 ";
                        int i = 0; string Stat = ""; var statsListForRandom = new List<string>(chrStats.Keys);
                        while (i <= 1)
                        {
                            int randomStatIndex = rnd.Next(statsListForRandom.Count);
                            string randomStat = statsListForRandom[randomStatIndex];
                            if (randomStat != "Charisma")
                            {
                                if (Stat != randomStat)
                                {
                                    chrStats[randomStat] += 1;
                                    Stat = randomStat;
                                    bonusRace += $"{randomStat} +1 ";

                                }
                                else
                                    continue;
                            }
                            else
                                continue;
                            i++;
                        }
                        break;
                    case Races.Halfling:
                        chrStats["Dexterity"] += 2;
                        bonusRace = "Dexterity +2";
                        break;
                    case Races.HalfOrc:
                        chrStats["Strength"] += 2;
                        chrStats["Constitution"] += 1;
                        bonusRace = "Strength +2 Constitution +1";
                        break;
                    case Races.Tiefling:
                        chrStats["Charisma"] += 2;
                        chrStats["Intelligence"] += 1;
                        bonusRace = "Charisma +2 Intelligence +1";
                        break;
                    case Races.Dragonborn:
                        chrStats["Strength"] += 2;
                        chrStats["Charisma"] += 1;
                        bonusRace = "Strength +2 Charisma +1";
                        break;
                }
            }
            public int[] GetModifier()
            {
                List<string> statsKeys = new List<string>(chrStats.Keys);
                List<int> statsValues = new List<int>(chrStats.Values);
                int[] modifiers = new int[chrStats.Count];
                for (int i = 0; i < statsValues.Count; i++) { modifiers[i] = (int)Math.Floor((statsValues[i] - 10) / 2.0); }
                return modifiers;
            }
            public int[] GetSavingThrow(Classes stclass, int[] modifiers)
            {
                int[] savingThrows = (int[])modifiers.Clone();
                switch (stclass)
                {
                    case Classes.Bard:
                        savingThrows[1] += 2; // Dexterity
                        savingThrows[5] += 2; // Charisma
                        break;
                    case Classes.Fighter:
                        savingThrows[0] += 2; // Strength
                        savingThrows[2] += 2; // Constitution
                        break;
                    case Classes.Barbarian:
                        savingThrows[0] += 2; // Strength
                        savingThrows[2] += 2; // Constitution
                        break;
                    case Classes.Druid:
                        savingThrows[3] += 2; // Intelligence
                        savingThrows[4] += 2; // Wisdom
                        break;
                    case Classes.Sorcerer:
                        savingThrows[2] += 2; // Constitution
                        savingThrows[5] += 2; // Charisma
                        break;
                    case Classes.Cleric:
                        savingThrows[4] += 2; // Wisdom
                        savingThrows[5] += 2; // Charisma
                        break;
                    case Classes.Wizard:
                        savingThrows[3] += 2; // Intelligence
                        savingThrows[4] += 2; // Wisdom
                        break;
                    case Classes.Monk:
                        savingThrows[0] += 2; // Strength
                        savingThrows[1] += 2; // Dexterity
                        break;
                    case Classes.Paladin:
                        savingThrows[4] += 2; // Wisdom
                        savingThrows[5] += 2; // Charisma
                        break;
                    case Classes.Rogue:
                        savingThrows[3] += 2; // Intelligence
                        savingThrows[1] += 2; // Dexterity
                        break;
                    case Classes.Ranger:
                        savingThrows[0] += 2; // Strength
                        savingThrows[1] += 2; // Dexterity
                        break;
                    case Classes.Warlock:
                        savingThrows[4] += 2; // Wisdom
                        savingThrows[5] += 2; // Charisma
                        break;
                }
                return savingThrows;
            }
            public int GetMaxHits(Classes stclass, int[] modifiers)
            {
                int maxHits = 0;
                int durabilityStat = modifiers[2];
                switch (stclass)
                {
                    case Classes.Bard:
                        maxHits = 8 + durabilityStat;
                        break;
                    case Classes.Fighter:
                        maxHits = 10 + durabilityStat;
                        break;
                    case Classes.Barbarian:
                        maxHits = 12 + durabilityStat;
                        break;
                    case Classes.Druid:
                        maxHits = 8 + durabilityStat;
                        break;
                    case Classes.Sorcerer:
                        maxHits = 6 + durabilityStat;
                        break;
                    case Classes.Cleric:
                        maxHits = 8 + durabilityStat;
                        break;
                    case Classes.Wizard:
                        maxHits = 6 + durabilityStat;
                        break;
                    case Classes.Monk:
                        maxHits = 8 + durabilityStat;
                        break;
                    case Classes.Paladin:
                        maxHits = 10 + durabilityStat;
                        break;
                    case Classes.Rogue:
                        maxHits = 8 + durabilityStat;
                        break;
                    case Classes.Ranger:
                        maxHits = 10 + durabilityStat;
                        break;
                    case Classes.Warlock:
                        maxHits = 8 + durabilityStat;
                        break;
                }
                return maxHits;
            }
            public string GetHitDice(Classes chrClass) 
            {
                string Dice = "";
                switch (chrClass)
                {
                    case Classes.Bard:
                        Dice = "d8";
                        break;
                    case Classes.Fighter:
                        Dice = "d10";
                        break;
                    case Classes.Barbarian:
                        Dice = "d12";
                        break;
                    case Classes.Druid:
                        Dice = "d8";
                        break;
                    case Classes.Sorcerer:
                        Dice = "d6";
                        break;
                    case Classes.Cleric:
                        Dice = "d8";
                        break;
                    case Classes.Wizard:
                        Dice = "d6";
                        break;
                    case Classes.Monk:
                        Dice = "d8";
                        break;
                    case Classes.Paladin:
                        Dice = "d10";
                        break;
                    case Classes.Rogue:
                        Dice = "d8";
                        break;
                    case Classes.Ranger:
                        Dice = "d10";
                        break;
                    case Classes.Warlock:
                        Dice = "d8";
                        break;
                }
                return "1"+Dice; 
            }
            List<string> skillsList = new List<string>
            {
                "Athletics",        // 0 Strength
                "Acrobatics",       // 1 Dexterity
                "Sleight of Hand",  // 2 Dexterity
                "Stealth",          // 3 Dexterity
                "Arcana",           // 4 Intelligence
                "History",          // 5 Intelligence
                "Investigation",    // 6 Intelligence
                "Nature",           // 7 Intelligence
                "Religion",         // 8 Intelligence
                "Animal Handling",  // 9 Wisdom
                "Insight",          // 10 Wisdom
                "Medicine",         // 11 Wisdom
                "Perception",       // 12 Wisdom
                "Survival",         // 13 Wisdom
                "Deception",        // 14 Charisma
                "Intimidation",     // 15 Charisma
                "Performance",      // 16 Charisma
                "Persuasion"        // 17 Charisma
            };

            public Dictionary<string, int> GetSkilsModifaers(Classes stclass, int[] modifiers) 
            {
                int[] SkillModifaersNums = new int[skillsList.Count()];
                string[] SkillModifaersNames = skillsList.ToArray();
                for (int i = 0; i < SkillModifaersNums.Length; i++)
                {
                    if (i == 0) SkillModifaersNums[i] += modifiers[0];
                    if (i >= 1 && i <= 3) SkillModifaersNums[i] += modifiers[1];
                    if (i >= 4 && i <= 8) SkillModifaersNums[i] += modifiers[3];
                    if (i >= 9 && i <= 13) SkillModifaersNums[i] += modifiers[4];
                    if (i >= 14) SkillModifaersNums[i] += modifiers[5];
                }
                Dictionary<string, int> SkillModifaers = new Dictionary<string, int>();
                for (int i = 0; i < SkillModifaersNums.Length; i++) 
                {
                    SkillModifaers.Add(SkillModifaersNames[i], SkillModifaersNums[i]);
                }
                return GetSkilsBonus(SkillModifaers, stclass);
            }
            public Dictionary<string, int> bonusСalculation(List<int> IntsOfClassSkills, List<string> NamesOfClassSkills, List<string> NamesOfClassBonuses, int countOfBonuses) 
            {
                Dictionary<string, int> SkilsBonusModifaers = new Dictionary<string, int>();
                var BonusForRouge = new List<string>();
                int i = 0;
                int b = countOfBonuses;
                if (b > 4)
                    throw new ArgumentOutOfRangeException("Enter a valid number, dummy:)");
                string StatChecker = "";
                while (i < b)
                {
                    int rndIndex = rnd.Next(NamesOfClassBonuses.Count);
                    string rndStat = NamesOfClassBonuses[rndIndex];
                    if (!StatChecker.Contains(rndStat))
                    {
                        if (b == 4) 
                        {
                            BonusForRouge.Add(rndStat);
                        }
                        int IntsIndex = 0;
                        for (int j = 0; j < NamesOfClassSkills.Count; j++)
                        {
                            if (rndStat == NamesOfClassSkills[j])
                            {
                                IntsIndex = j;
                            }
                        }
                        IntsOfClassSkills[IntsIndex] += 2;
                        StatChecker += rndStat;
                    }
                    else continue;
                    i++;
                }
                if (BonusForRouge.Count > 0) 
                {
                    bonusСalculation(IntsOfClassSkills, NamesOfClassSkills, BonusForRouge, 2);
                }
                for (int j = 0; j < NamesOfClassSkills.Count; j++)
                {
                    SkilsBonusModifaers.Add(NamesOfClassSkills[j], IntsOfClassSkills[j]);
                }
                return SkilsBonusModifaers;
            }
            public Dictionary<string, int> GetSkilsBonus(Dictionary<string, int> SkillModifaers, Classes stclass)
            {
                Dictionary<string, int> SkilsBonusModifaers = new Dictionary<string, int>();
                List<int> Ints = new List<int>(SkillModifaers.Values);
                List<string> Names = new List<string>(SkillModifaers.Keys);
                switch (stclass)
                {
                    case Classes.Bard:
                        var BardNames = new List<string>(Names);
                        SkilsBonusModifaers = bonusСalculation(Ints, Names, BardNames, 3);
                        break;

                    case Classes.Fighter:
                        var FighterNames = new List<string>() {
                            Names[0],
                            Names[1],
                            Names[9],
                            Names[10],
                            Names[15],
                            Names[12],
                            Names[13],
                            };
                        SkilsBonusModifaers = bonusСalculation(Ints, Names, FighterNames, 2);
                        break;
                    case Classes.Barbarian:
                        var BarbarianNames = new List<string>() {
                            Names[0],
                            Names[15],
                            Names[12],
                            Names[13],
                            Names[9],
                            Names[7],
                            };
                        SkilsBonusModifaers = bonusСalculation(Ints, Names, BarbarianNames, 2);
                        break;
                    case Classes.Druid:
                        var DruidNames = new List<string>() {
                            Names[4],
                            Names[9],
                            Names[10],
                            Names[11],
                            Names[7],
                            Names[12],
                            Names[13],
                            };
                        SkilsBonusModifaers = bonusСalculation(Ints, Names, DruidNames, 2);
                        break;
                    case Classes.Sorcerer:
                        var SorcererNames = new List<string>() {
                            Names[4],
                            Names[14],
                            Names[10],
                            Names[15],
                            Names[16],
                            Names[17],
                            };
                        SkilsBonusModifaers = bonusСalculation(Ints, Names, SorcererNames, 2);
                        break;
                    case Classes.Cleric:
                        var ClericNames = new List<string>() {
                            Names[5],
                            Names[10],
                            Names[11],
                            Names[17],
                            Names[8],
                            };
                        SkilsBonusModifaers = bonusСalculation(Ints, Names, ClericNames, 2);
                        break;
                    case Classes.Wizard:
                        var WizardNames = new List<string>() {
                            Names[4],
                            Names[5],
                            Names[10],
                            Names[6],
                            Names[8],
                            };
                        SkilsBonusModifaers = bonusСalculation(Ints, Names, WizardNames, 2);
                        break;
                    case Classes.Monk:
                        var MonkNames = new List<string>() {
                            Names[0],
                            Names[10],
                            Names[15],
                            Names[11],
                            Names[17],
                            Names[8],
                            };
                        SkilsBonusModifaers = bonusСalculation(Ints, Names, MonkNames, 2);
                        break;
                    case Classes.Paladin:
                        var PaladinNames = new List<string>() {
                            Names[0],
                            Names[10],
                            Names[15],
                            Names[11],
                            Names[17],
                            Names[8],
                            };
                        SkilsBonusModifaers = bonusСalculation(Ints, Names, PaladinNames, 2);
                        break;
                    case Classes.Rogue:
                        var RogueNames = new List<string>() {
                            Names[0],
                            Names[1],
                            Names[14],
                            Names[10],
                            Names[15],
                            Names[6],
                            Names[12],
                            Names[16],
                            Names[17],
                            Names[2],
                            Names[3]
                            };
                        SkilsBonusModifaers = bonusСalculation(Ints, Names, RogueNames, 4);
                        break;
                    case Classes.Ranger:
                        var RangerNames = new List<string>() {
                            Names[1],
                            Names[0],
                            Names[10],
                            Names[6],
                            Names[3],
                            Names[13],
                            };
                        SkilsBonusModifaers = bonusСalculation(Ints, Names, RangerNames, 3);
                        break;
                    case Classes.Warlock:
                        var WarlockNames = new List<string>() {
                            Names[4],
                            Names[14],
                            Names[5],
                            Names[15],
                            Names[6],
                            Names[7],
                            Names[8]
                            };
                        SkilsBonusModifaers = bonusСalculation(Ints, Names, WarlockNames, 2);
                        break;
                }
                return SkilsBonusModifaers;
            }
            public int GetPassivePerception(Dictionary<string, int> SkilsBonusModifaers)
            {
                return 10 + SkilsBonusModifaers["Perception"];
            }
            public int GetSpeed(Races chrRace)
            {
                int speed = 30;
                if (chrRace == Races.Gnome || chrRace == Races.Dwarf || chrRace == Races.Halfling) speed = 25;
                return speed;
            }
            public Character(string name, Genders gender, Races chrRace, Classes chrClass)
            {
                this.gender = gender;
                this.name = name;
                this.chrRace = chrRace;
                this.chrClass = chrClass;
                this.chrStats = new Dictionary<string, int>
                {
                    { "Strength", PullRandomStat()},
                    { "Dexterity", PullRandomStat()},
                    { "Constitution", PullRandomStat()},
                    { "Intelligence", PullRandomStat()},
                    { "Wisdom", PullRandomStat()},
                    { "Charisma", PullRandomStat()}
                };
                ImprovingAbilities(chrRace);
            }

            public void Print()
            {
                int[] modifiers = GetModifier();
                int[] savingThrows = GetSavingThrow(chrClass, modifiers);
                int maxHits = GetMaxHits(chrClass, modifiers);
                var skillsModifiers = GetSkilsModifaers(chrClass, modifiers);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                                 CHARACTER SHEET                            ║");
                Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("┌────────────────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("│             BASIC INFORMATION                                              │");
                Console.WriteLine("└────────────────────────────────────────────────────────────────────────────┘");
                Console.ResetColor();
                Console.WriteLine($"Name:                    {name,-35} ");
                Console.WriteLine($"Gender:                  {gender,-35} ");
                Console.WriteLine($"Race:                    {chrRace,-35} ");
                Console.WriteLine($"Racial Bonus:            {bonusRace,-35} ");
                Console.WriteLine($"Class:                   {chrClass,-35} ");
                Console.WriteLine($"LVL:                     {level,-35} ");
                Console.WriteLine($"Max Hits:                {maxHits,-35} ");
                Console.WriteLine($"Hit Dice:                {GetHitDice(chrClass),-35} ");
                Console.WriteLine($"Speed:                   {GetSpeed(chrRace),-35} ");
                Console.WriteLine($"Initiative:              {(modifiers[1] > 0 ? "+" : "")}{modifiers[1],-34} ");
                Console.WriteLine($"Passive Perception:      {GetPassivePerception(skillsModifiers),-35} ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("┌────────────────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("│             STATS | MODIFIERS | SAVING THROWS                              │");
                Console.WriteLine("└────────────────────────────────────────────────────────────────────────────┘");
                Console.ResetColor();

                int i = 0;

                foreach (KeyValuePair<string, int> entry in chrStats)
                {
                    Console.WriteLine($"{entry.Key,-12}: {entry.Value,2}          {modifiers[i],2}             {savingThrows[i],2}            ");
                    i++;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("┌────────────────────────────────────────────────────────────────────────────┐");
                Console.WriteLine("│             SKILLS                                                         │");
                Console.WriteLine("└────────────────────────────────────────────────────────────────────────────┘");
                Console.ResetColor();


                foreach (var skill in skillsModifiers)
                {
                    Console.WriteLine($"• {skill.Key,-20}: {skill.Value,2}");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("If the character is good, type '1' and take it. If it's trash, type anything else to generate the next one.");
            System.Threading.Thread.Sleep(3000);
            while (true)
            {
                Console.Clear();
                Genders myGender = (Genders)rnd.Next(2);
                string myName = getRandomName(myGender);
                Races myRace = (Races)rnd.Next(9);
                Classes myClass = (Classes)rnd.Next(12);
                Character сharacter = new Character(myName, myGender, myRace, myClass);
                сharacter.Print();
                var mormPers = Console.ReadLine();
                if (mormPers == "1")
                {
                    break;
                }
            }
        }
    }
}
