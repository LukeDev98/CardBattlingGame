using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_19_Assignment_2
{
    public class Display
    {
        
        protected void DisplayInformation(string name, int hp, int at, int dp, int dc, int mana, string ability)        //Taking in either card details or player details
        {
            string hpstring = hp.ToString();
            string atstring = at.ToString();
            string dpstring = dp.ToString();
            string dcstring = dc.ToString();
            string manastring = mana.ToString();

            string abilitytext;
            string attext;
            string dptext;
            string dctext;
            string manatext;

            string nametext = "|Name: " + name;
            string hptext = "|Health: " + hpstring;

            if (ability == "null")
            {
                attext = "|Cards Destroyed: " + atstring;
            }
            else
            {
                attext = "|Attack: " + atstring;
            }

            if (ability == "null")
            {
                dptext = "|Cards Lost: " + dpstring;
            }
            else
            {
                dptext = "|Defence: " + dpstring;
            }

            if (ability == "null")
            {
                dctext = "|Mana Available: " + dcstring;
            }
            else
            {
                dctext = "|Evade Chance: " + dcstring;
            }

            if (ability == "null")
            {
                manatext = "|";
            }
            else
            {
                manatext = "|Mana Cost: " + manastring;
            }
            
            if (ability == "null")
            {
                abilitytext = "|";
            }
            else
            {
                abilitytext = "|Ability: " + ability;
            }



            

            while (nametext.Length <= 26)           //Following loops for adding | character to end of the string
            {
                if (nametext.Length < 26)
                {
                    nametext += " ";
                }
                if (nametext.Length == 26)
                {
                    nametext += "|";
                }
            }
            while (hptext.Length <= 26)
            {
                if (hptext.Length < 26)
                {
                    hptext += " ";
                }
                if (hptext.Length == 26)
                {
                    hptext += "|";
                }
            }
            while (attext.Length <= 26)
            {
                if (attext.Length < 26)
                {
                    attext += " ";
                }
                if (attext.Length == 26)
                {
                    attext += "|";
                }
            }
            while (dptext.Length <= 26)
            {
                if (dptext.Length < 26)
                {
                    dptext += " ";
                }
                if (dptext.Length == 26)
                {
                    dptext += "|";
                }
            }
            while (dctext.Length <= 26)
            {
                if (dctext.Length < 26)
                {
                    dctext += " ";
                }
                if (dctext.Length == 26)
                {
                    dctext += "|";
                }
            }
            while (manatext.Length <= 26)
            {
                if (manatext.Length < 26)
                {
                    manatext += " ";
                }
                if (manatext.Length == 26)
                {
                    manatext += "|";
                }
            }
            while (abilitytext.Length <= 26)
            {
                if (abilitytext.Length < 26)
                {
                    abilitytext += " ";
                }
                if (abilitytext.Length == 26)
                {
                    abilitytext += "|";
                }
            }

            
            if (Processes.Global == false)                      //Display card 
            {
                Messages.Display(" =========================");
                Messages.Display(nametext);
                Messages.Display(hptext);
                Messages.Display(attext);
                Messages.Display(dptext);
                Messages.Display(dctext);
                Messages.Display(manatext);
                Messages.Display(abilitytext);
                Messages.Display(" =========================");
            }
            else
            {
                Messages.GlobalDisplay(" =========================");               //Displaying card globally
                Messages.GlobalDisplay(nametext);
                Messages.GlobalDisplay(hptext);
                Messages.GlobalDisplay(attext);
                Messages.GlobalDisplay(dptext);
                Messages.GlobalDisplay(dctext);
                Messages.GlobalDisplay(manatext);
                Messages.GlobalDisplay(abilitytext);
                Messages.GlobalDisplay(" =========================");
                Processes.Global = false;
            }
            

        }
    }
    
}
