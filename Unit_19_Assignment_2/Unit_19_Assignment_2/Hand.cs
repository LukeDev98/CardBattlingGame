using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_19_Assignment_2
{
    public static class Hand
    { 
        
        public static void Fill(Player player)          //Filling the hand from player's deck
        {
            
            Messages.Message("Filling Hand");
            while (player.Hand.Count <= 8)
            {
                int tempcount = player.Hand.Count;
                player.Hand.Add(player.deck[0]);
                player.HandNames[tempcount] = player.Hand[tempcount].Name;
                
                player.HandID[tempcount] = player.Hand[tempcount].ID;
                player.deck.RemoveAt(0);                                //Removing added card from deck
                if (player.Hand.Count == 8)
                {
                    break;
                }
            }
            Messages.Message("Hand filled!");
            DisplayHand(player);
        }
        public static void DisplayHand(Player player)                               //displaying cards in the hand
        {
            Messages.Display(" ");
            Messages.Display(player.Name + ", these are the cards in your hand: ");
            int count = 0;
            while (count <= player.Hand.Count - 1)
            {
                Processes.Global = false;
                player.Hand[count].CardDisplay();
                Messages.Display("Card ID: " + player.HandID[count].ToString());
                Messages.Display(" ");
                count += 1;
            }
            Messages.Display(player.Name + ", your available mana is " + player.Mana.ToString());
            SelectCard(player);
        }
        public static void SelectCard(Player player)                    //Choosing a card to use
        {
            bool Correct = false;
            while (Correct == false)
            {
                Messages.Display(" ");
                Messages.Display("Which card would you like to select? please use the ID under each card to select your card.");
                player.placeanswer = Messages.Input();

                try
                {
                    int count = 1;
                    while (count <= player.Hand.Count)
                    {
                        if (Int32.Parse(player.placeanswer) == player.Hand[count - 1].ID)
                        {
                            player.cardselectnumber = count - 1;
                            count += 1;
                            break;
                        }
                        else
                        {
                            count += 1;
                        }
                    }
                    Card card = player.Hand[player.cardselectnumber];
                    //player.Hand.Remove(player.Hand[cardselectnumber]);
                    Correct = true;
                    CardPlace(player, card);
                    break;
                }
                catch
                {
                    if (player.choice == false)
                    {
                        Messages.Display("Please enter a number.");
                        Correct = false;
                    }
                    else
                    {
                        Correct = true;
                        player.choice = false;
                    }
                }
            }
        }
        public static void CardPlace(Player player, Card card)              //Deciding what to do with chosen card
        {
            bool Battle = false;
            while (Battle == false)
            {
                bool Continue = false;
                while (Continue == false)
                {
                    Messages.Display("The card you have selected is " + card.Name + "  ID: " + card.ID);
                    Messages.Display("What would you like to do with this " + card.Name + "? You can place the card (type 'place'), " + '\n' + " return it to the hand (type 'return'), view the details of this card (type 'view'), or view your stats(type 'stats'): ");
                    string answer = Messages.Input();
                    if (answer == "Place" || answer == "place")
                    {
                        if (player.Mana >= card.mana)
                        {
                            try
                            {
                                Messages.Display("Placing " + card.Name + ".");
                                player.CardsPlaced.Add(card);
                                player.Hand.Remove(card);
                                Messages.Display(player.Hand.Count.ToString());
                                player.Mana = player.Mana - card.mana;
                                card = null;
                                Continue = true;
                            }
                            catch (Exception exception)
                            {
                                string ex = exception.ToString();
                                Messages.Display(ex);
                            }
                            
                        }
                        else
                        {
                            Messages.Display("You don't have enough mana to place that card. The selected card requires " + card.mana + " mana, you have " + player.Mana + " mana.");
                            Continue = false;
                        }

                    }
                    else if (answer == "Return" || answer == "return")
                    {
                        Messages.Display("Returning " + card.Name + " to the deck.");
                        card = null;
                        Continue = true;
                    }
                    else if (answer == "View" || answer == "view")
                    {
                        card.CardDisplay();
                        Continue = false;
                    }
                    else if (answer == "Stats" || answer == "stats")
                    {
                        Processes.Global = false;
                        player.PlayerDisplay();
                        Continue = false;
                    }
                    else
                    {
                        Messages.Display("Invalid input, please try again");
                        Continue = false;
                    }
                }
                bool Confirm = false;
                while (Confirm == false)
                {
                    try
                    {
                        Messages.Display("Would you like to end the Selection-Phase? You can't palce cards after this until your next turn (yes/no): ");            //Choosing to enter the battle phase of the turn
                        string answer = Messages.Input();
                        switch (answer)
                        {
                            case "Yes":
                            case "yes":
                                Battle = true;
                                Confirm = true;
                                Messages.Display("Entering Battle-Phase");
                                player.choice = true;
                                break;
                            case "No":
                            case "no":
                                Battle = false;
                                Confirm = true;
                                Messages.Display("Returning to Card Selector");
                                DisplayHand(player);
                                break;
                        }
                    }
                    catch
                    {
                        Messages.Display("That's not a valid input, please try again.");
                        Confirm = false;
                    }
                }
                
            }
            //METHOD FOR RUNNING ARENA GOES HERE
        }
    }
}
