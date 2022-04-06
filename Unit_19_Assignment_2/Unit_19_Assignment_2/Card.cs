using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_19_Assignment_2
{


    public class Card : Display
    {
        public int ID;
        public string Name;
        public int Health;
        public int Attack;
        public int Defence;
        public int DefenceChance;
        public int mana;
        public string Ability;
        public Card(string name)
        {
            switch (name)           //Setting attributes depending on card type
            {
                case "Peasant":
                    Name = name;
                    Health = 25;
                    Attack = 25;
                    Defence = 16;
                    DefenceChance = 3;
                    mana = 1;
                    Ability = "Frenzy";
                    break;
                case "Soldier":
                    Name = name;
                    Health = 35;
                    Attack = 42;
                    Defence = 21;
                    DefenceChance = 4;
                    mana = 2;
                    Ability = "Crippling Blow";
                    break;
                case "Medic":
                    Name = name;
                    Health = 60;
                    Attack = 31;
                    Defence = 20;
                    DefenceChance = 5;
                    mana = 3;
                    Ability = "Team Heal";
                    break;
                case "Archer":
                    Name = name;
                    Health = 41;
                    Attack = 30;
                    Defence = 17;
                    DefenceChance = 6;
                    mana = 3;
                    Ability = "Suppresive Fire";
                    break;
                case "Ninja":
                    Name = name;
                    Health = 52;
                    Attack = 46;
                    Defence = 16;
                    DefenceChance = 6;
                    mana = 4;
                    Ability = "Smoke Grenade";
                    break;
                case "Knight":
                    Name = name;
                    Health = 77;
                    Attack = 41;
                    Defence = 20;
                    DefenceChance = 10;
                    mana = 5;
                    Ability = "Get Behind Me!";
                    break;
                case "Orc":
                    Name = name;
                    Health = 68;
                    Attack = 47;
                    Defence = 15;
                    DefenceChance = 8;
                    mana = 5;
                    Ability = "Beserker";
                    break;
                case "ElvenArcher":
                    Name = "Elven Archer";
                    Health = 82;
                    Attack = 49;
                    Defence = 20;
                    DefenceChance = 8;
                    mana = 6;
                    Ability = "Bullseye";
                    break;
                case "Mage":
                    Name = name;
                    Health = 100;
                    Attack = 58;
                    Defence = 20;
                    DefenceChance = 7;
                    mana = 6;
                    Ability = "Lightning Strike";
                    break;
            }
    }
        public void CardDisplay()
        {
            DisplayInformation(Name, Health, Attack, Defence, DefenceChance, mana, Ability);            //Displays card stats
        }
        public void CardImage(string name)                                  //Displays information about card type
        {
            //string imgPath = @"H:\\HNC\\Marjory\\Visual Studio 2015\\Projects\\ImageOpenTest\\Shronk.png";
            System.Diagnostics.Process.Start(@"H:\\HNC\\Marjory\\Visual Studio 2015\\Projects\\Unit_19_Assignment_2\\" + Name + ".png");
        }
        
    }
}

