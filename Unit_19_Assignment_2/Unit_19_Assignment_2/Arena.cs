using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_19_Assignment_2
{
    public static class Arena
    {
        public static void DisplayArena(Player player, Player opponent)             //Method for displaying the arena and cards placed
        {
            Messages.GlobalDisplay(" ");
            Messages.GlobalDisplay(" ");
            Messages.GlobalDisplay(" ");
            Messages.GlobalDisplay(" ");
            Messages.GlobalDisplay("--------------------------------------------------------------ARENA--------------------------------------------------------------");
            Messages.GlobalDisplay("=================================================================================================================================");
            Messages.GlobalDisplay("                                             v " + opponent.Name + "  HP: " + opponent.Health);
            Messages.GlobalDisplay("=================================================================================================================================");
            Messages.GlobalDisplay(" ");
            if (opponent.CardsPlaced.Count != 0)
            {
                int count = 0;
                while (count <= opponent.CardsPlaced.Count - 1)
                {
                    Processes.Global = true;
                    opponent.CardsPlaced[count].CardDisplay();
                    Messages.GlobalDisplay("Card ID: " + opponent.CardsPlaced[count].ID.ToString());
                    Messages.GlobalDisplay(" ");
                    count += 1;
                }
            }
            else
            {
                Messages.GlobalMessage(opponent.Name + " has no cards placed, they can be attacked directly!");
            }

            Messages.GlobalDisplay("=================================================================================================================================");
            Messages.GlobalDisplay("=================================================================================================================================");
            Messages.GlobalDisplay(" ");
            if (player.CardsPlaced.Count != 0)
            {
                int count = 0;
                while (count <= player.CardsPlaced.Count - 1)
                {
                    Processes.Global = true;
                    player.CardsPlaced[count].CardDisplay();
                    Messages.GlobalDisplay("Card ID: " + player.CardsPlaced[count].ID.ToString());
                    Messages.GlobalDisplay(" ");
                    count += 1;
                }
            }
            else
            {
                Messages.GlobalMessage(player.Name + " has placed no cards down, they can be attacked directly!");
            }
            Messages.GlobalDisplay("=================================================================================================================================");
            Messages.GlobalDisplay("                                             ^ " + player.Name + "  HP: " + player.Health);
            Messages.GlobalDisplay("=================================================================================================================================");
            Messages.GlobalDisplay("---------------------------------------------------------------------------------------------------------------------------------");
            Messages.GlobalDisplay(" ");
            Messages.GlobalDisplay(" ");
            Messages.GlobalDisplay(" ");
            Messages.GlobalDisplay(" ");
        }
        public static void BattleArena(Player player, Player opponent)
        {
            if (player.CardsPlaced.Count > 0)                                                   //If player has placed cards
            {
                Messages.Display(player.Name + ", you can attack now!");
                Messages.Display(" ");
                int count = 0;
                //Card playcard = player.CardsPlaced[count];
                
                bool Correct = false;
                while (Correct == false)
                {
                    if (count <= player.CardsPlaced.Count - 1)                                  //If player still has cards to play
                    {
                        Card playcard = player.CardsPlaced[count];

                        Messages.Display(player.Name + ", your attacking card is " + playcard.Name + " ID: " + playcard.ID);
                        Messages.Display(" ");
                        Messages.Display("Would you like to 'attack', 'pass', or use ability? ability timer must be equal to or greater than 2:");      //Choosing to attack, pass, or use ability
                        Messages.Display("Ability Timer: " + player.Ability_timer);
                        string answer = Messages.Input();
                        if (answer == "Attack" || answer == "attack")
                        {
                            if (opponent.CardsPlaced.Count > 0)                                 //If opponent has more than 0 cards placed
                            {
                                Messages.Display(" ");
                                Messages.Display("You have chosen to attack a card.");
                                bool Confirm = false;
                                while (Confirm == false)
                                {
                                    try
                                    {
                                        Messages.Display("When choosing a card, please use the ID number");             //Choosing the card to attack
                                        Messages.Display(" ");
                                        Messages.Display("Which card would you like to attack?");
                                        Messages.Display(" ");
                                        string answer2 = Messages.Input();

                                        int count2 = 0;
                                        while (count2 <= opponent.CardsPlaced.Count - 1)                            //Loop for comparing input to available cards
                                        {
                                            if (Int32.Parse(answer2) == opponent.CardsPlaced[count2].ID)
                                            {
                                                player.targetcard = opponent.CardsPlaced[count2];
                                                opponent.CardsPlaced.RemoveAt(count2);
                                                break;
                                            }
                                            else
                                            {
                                                count2 += 1;
                                            }
                                        }
                                        CardAttack(player, opponent, playcard, player.targetcard);                  //RUN CARD ATTACK METHOD
                                        Confirm = true;
                                        count += 1;
                                    }
                                    catch
                                    {
                                        Messages.Display("Invalid input, please try again.");
                                        Confirm = false;
                                    }
                                }
                            }
                            else if (opponent.CardsPlaced.Count == 0)                                   //Condition for attacking directly
                            {
                                Messages.Display(" ");
                                Messages.Display("Opponent has no cards placed! Attacking directly!");
                                DirectAttack(player, opponent, playcard);
                                count += 1;                                                                         //RUN DIRECT ATTACK METHOD
                            }

                            //BOTTOM OF IF STATEMENT
                        }
                        else if (answer == "Pass" || answer == "pass")                                  //If choosing to pass (not attack), go to next card
                        {
                            Correct = true;
                            count += 1;     //BOTTOM OF IF STATEMENT
                        }
                        else if (answer == "Ability" || answer == "ability")            //Lets player use ability instead of attack if ability timer is 2 or higher
                        {
                            if (player.Ability_timer >= 2)
                            {
                                bool Confirm = false;
                                while (Confirm == false)
                                {
                                    try
                                    {
                                        Messages.Display("When choosing a card, please use the ID number");
                                        Messages.Display(" ");
                                        Messages.Display("Which card would you like to attack?");
                                        Messages.Display(" ");
                                        string answer2 = Messages.Input();

                                        int count2 = 0;
                                        while (count2 <= opponent.CardsPlaced.Count - 1)                            //Loop for comparing input to available cards
                                        {
                                            if (Int32.Parse(answer2) == opponent.CardsPlaced[count2].ID)
                                            {
                                                player.targetcard = opponent.CardsPlaced[count2];
                                                opponent.CardsPlaced.RemoveAt(count2);
                                                break;
                                            }
                                            else
                                            {
                                                count2 += 1;
                                            }
                                        }
                                        UseAbility(player, opponent, playcard, player.targetcard);                                        //RUN CARD ATTACK METHOD
                                        Confirm = true;
                                    }
                                    catch
                                    {
                                        Messages.Display("Invalid input, please try again.");
                                        Confirm = false;
                                    }
                                }
                                count += 1;
                                Correct = true;
                            }
                            else
                            {
                                Messages.Display("Not long enough since last ability use, you need to wait " + (2 - player.Ability_timer) + " turn(s)");
                                Correct = false;
                            }

                        }
                        else
                        {
                            Messages.Display("Invalid input, please try again.");
                            Correct = false;
                        }
                    }
                    else
                    {                                                                                    //Ending turn if no cards are left
                        Messages.GlobalDisplay("Ending turn...");
                        Correct = true;
                    }
                    if (player.IsWon == true)
                    {
                        break;
                    } 
                }
            }
            else if (player.CardsPlaced.Count == 0)                                                     //Ending turn if no cards in first place
            {
                Messages.GlobalDisplay(player.Name + " has no cards placed and cannot attack. Turn ending...");
            }
        }

        public static void CardAttack(Player player, Player opponent, Card playcard, Card targetcard)
        {
            Random random = new Random();
            int number = random.Next(1, 101);
            if (number > targetcard.DefenceChance)
            {
                int TargetDamageRecieved = playcard.Attack - targetcard.Defence;                                    //Total damage recieved after defence
                Messages.GlobalDisplay(opponent.Name + "'s " + targetcard.Name + " has recieved " + TargetDamageRecieved + " damage");
                int NewHealthCard = targetcard.Health - TargetDamageRecieved;                                       //New health of the card
                int PlayerAttackDamage;
                
                targetcard.Health = NewHealthCard;
                if (targetcard.Health <= 0)
                {
                    Messages.GlobalDisplay(targetcard.Name + " has been destroyed!");
                    player.cardsdestroyed += 1;
                    opponent.cardslost += 1;
                }
                else if (targetcard.Health > 0)
                {
                    opponent.CardsPlaced.Add(targetcard);
                }
                if (targetcard.Health < 0)
                {
                    PlayerAttackDamage = TargetDamageRecieved - targetcard.Health;                                  //Damage to opponent's health if damage is greater than card health
                    Messages.GlobalDisplay(opponent.Name + " has recieved " + PlayerAttackDamage.ToString() + " damage to their health!");
                    opponent.Health = opponent.Health - PlayerAttackDamage;                                         //Setting new opponent health
                    Messages.GlobalDisplay(opponent.Name + "'s health is now " + opponent.Health + " HP");
                }
            }
            else
            {
                Messages.GlobalDisplay(targetcard.Name + " evaded the attack!, no damage recieved!");
                opponent.CardsPlaced.Add(targetcard);                                                               //return target card to the arena
            }
            if (opponent.Health <= 0)                                                                               //Check to see if game is won
            {
                player.IsWon = true;
            }

            DisplayArena(player, opponent);

            Messages.GlobalDisplay("Hit enter to continue.");
            Messages.GlobalDisplay(" ");
        }

        public static void DirectAttack(Player player, Player opponent, Card playcard)
        {
            Messages.GlobalDisplay(playcard.Name + " is attacking " + opponent.Name + " directly!");
            opponent.Health = opponent.Health - playcard.Attack;                                                   //Recieving damage from direct attack
            Messages.GlobalDisplay(opponent.Name + " has recieved " + playcard.Attack.ToString() + " damage!");
            Messages.GlobalDisplay("Their health is now " + opponent.Health.ToString() + " HP!");

            if (opponent.Health <= 0)                                                                               //Check to see if game is won
            {
                player.IsWon = true;
            }
            DisplayArena(player, opponent);
        }

        public static void UseAbility(Player player, Player opponent, Card playcard, Card targetcard)           //Method for applying effects of abilities
        {
            if (playcard.Ability == "Frenzy")
            {
                Messages.GlobalDisplay(player.Name + " has used Frenzy! Dealing 10 damage to each opponent card!");
                foreach (Card c in opponent.CardsPlaced)
                {
                    c.Health = c.Health = 10;

                    if (c.Health <= 0)
                    {
                        Messages.GlobalDisplay(c.Name + " has been destroyed!");
                        player.cardsdestroyed += 1;
                        opponent.cardslost += 1;
                        opponent.CardsPlaced.Remove(c);
                    }
                }
            }
            else if (playcard.Ability == "Crippling Blow")
            {
                Messages.GlobalDisplay(player.Name + " has used Crippling Blow! Dealing 50 damage to the target card!");
                targetcard.Health = targetcard.Health - 50;
                if (targetcard.Health <= 0)
                {
                    Messages.GlobalDisplay(targetcard.Name + " has been destroyed!");
                    player.cardsdestroyed += 1;
                    opponent.cardslost += 1;
                    opponent.CardsPlaced.Remove(targetcard);
                }

            }
            else if (playcard.Ability == "Team Heal")
            {
                Messages.GlobalDisplay(player.Name + " has used Team Heal! Healing " + player.Name + "'s cards by 20 HP!");
                foreach (Card c in player.CardsPlaced)
                {
                    c.Health += 20;
                }
            }
            else if (playcard.Ability == "Suppresive Fire")
            {
                Messages.GlobalDisplay(player.Name + " has used Suppresive Fire! opponent's cards lose 10 attack");
                foreach (Card c in opponent.CardsPlaced)
                {
                    c.Attack = c.Attack - 10;
                }
            }
            else if (playcard.Ability == "Smoke Grenade")
            {
                Messages.GlobalDisplay(player.Name + " has used Smoke Grenade! Evade chance increased by 20%!");
                playcard.DefenceChance += 20;
            }
            else if (playcard.Ability == "Get Behind Me!")
            {
                Messages.GlobalDisplay(player.Name + " has used 'Get Behind Me!' Their Knight recieves 10 more defence and their team recieves 10% more evade chance!");
                playcard.Defence += 10;
                foreach (Card c in player.CardsPlaced)
                {
                    c.DefenceChance += 10;
                }
            }
            else if (playcard.Ability == "Beserker")
            {
                Messages.GlobalDisplay(player.Name + " has used Beserker! Their Orc recieves 20 more attack!");
                playcard.Attack += 20;
            }
            else if (playcard.Ability == "Bullseye")
            {
                Messages.GlobalDisplay(player.Name + " has used Bullseye! Dealing 82 damage to selected card!");
                targetcard.Health = targetcard.Health - 82;
                if (targetcard.Health <= 0)
                {
                    Messages.GlobalDisplay(targetcard.Name + " has been destroyed!");
                    player.cardsdestroyed += 1;
                    opponent.cardslost += 1;
                    opponent.CardsPlaced.Remove(targetcard);
                }
            }
            else if (playcard.Ability == "Lightning Strike")
            {
                Messages.GlobalDisplay(player.Name + " has used Lightning Strike! Dealing 40 damage to each enemy card!");
                foreach (Card c in opponent.CardsPlaced)
                {
                    c.Health = c.Health - 40;
                    if (c.Health <= 0)
                    {
                        Messages.GlobalDisplay(c.Name + " has been destroyed!");
                        player.cardsdestroyed += 1;
                        opponent.cardslost += 1;
                        opponent.CardsPlaced.Remove(c);
                    }
                }
            }
            player.Ability_timer = 0;
        }
    }
}
