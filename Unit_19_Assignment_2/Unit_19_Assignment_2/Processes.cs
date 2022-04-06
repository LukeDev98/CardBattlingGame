using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_19_Assignment_2
{
    public static class Processes
    {
        public static int turn = 1;
        public static bool Global = false;
        public static void Turns(Player player1, Player player2)
        {
            player1.Menu();                                         //players log in then edit and fill their decks
            turn = 2;
            player2.Menu();
            turn = 1;
            Deck.Question(player1);
            turn = 2;
            Deck.Question(player2);
            turn = 1;

            while (turn < 4)
            {
                if (turn == 1)
                {
                    bool Continue = false;
                    while (Continue == false)
                    {
                        Player player = player1;
                        Messages.Display("Would you like to update your deck again? (yes/no): ");
                        string answer = Messages.Input();
                        switch (answer)
                        {
                            case "Yes":
                            case "yes":
                                Messages.Message("Re-entering editing mode...");
                                Deck.Update(player);
                                Continue = false;
                                break;
                            case "No":
                            case "no":
                                //case "no":
                                Messages.Message("Okay, continuing...");
                                Deck.Fill(player);
                                Continue = true;
                                break;
                        }
                    }
                    turn = 2;
                }
                else if (turn == 2)
                {
                    bool Continue = false;
                    while (Continue == false)
                    {
                        Player player = player2;
                        Messages.Display(player.Name + ", would you like to update your deck again? (yes/no): ");
                        string answer = Messages.Input();
                        switch (answer)
                        {
                            case "Yes":
                            case "yes":
                                Messages.Message("Re-entering editing mode...");
                                Deck.Update(player);
                                Continue = false;
                                break;
                            case "No":
                            case "no":
                                //case "no":
                                Messages.Message("Okay, continuing...");
                                Deck.Fill(player);
                                Continue = true;
                                break;
                        }
                    }
                    turn = 3;
                }
                else if (turn == 3)
                {
                    turn = 4;
                }

            }
            bool PlayAgain = true;
            while (PlayAgain == true)
            {
                turn = 1;
                bool firstturn = true;
                bool Play = true;
                while (Play == true)                            //Game begins
                {
                    if (turn == 1)
                    {
                        Player player = player1;
                        Player opponent = player2;

                        Hand.Fill(player);                          //Fill hand with cards
                        Arena.DisplayArena(player, opponent);
                        if (firstturn == false)
                        {
                            Arena.BattleArena(player, opponent);                                                    //BATTLE
                        }
                        else if (firstturn == true)
                        {
                            firstturn = false;
                        }
                        player.ManaCount += 1;
                        player.Mana = player.ManaCount;
                        turn = 2;
                        if (player.IsWon == true)                               //Condition for winning the game
                        {
                            Messages.GlobalMessage("PLAYER: " + player.Name + " HAS WON THE GAME!");
                            Play = false;

                        }
                        player.Ability_timer += 1;
                    }
                    else if (turn == 2)                 //Same as above but for player 2
                    {
                        Player player = player2;
                        Player opponent = player1;

                        Hand.Fill(player);
                        Arena.DisplayArena(player, opponent);
                        Arena.BattleArena(player, opponent);
                        player.ManaCount += 1;
                        player.Mana = player.ManaCount;
                        turn = 1;
                        if (player.IsWon == true)
                        {
                            Messages.GlobalMessage("PLAYER: " + player.Name + " HAS WON THE GAME!");
                            Play = false;

                        }
                        player.Ability_timer += 1;
                    }
                }

                bool count = true;
                while (count == true)
                {
                    Console.WriteLine("Would you like to play again? (yes/no)");                    //Asking player 1 if they want to play again
                    string answer = Console.ReadLine();
                    try
                    {
                        switch (answer)
                        {
                            case "yes":
                            case "Yes":
                                Messages.GlobalDisplay("Restarting");
                                count = false;
                                PlayAgain = true;
                                Deck.Fill(player1);
                                Deck.Fill(player2);

                                player1.HandID = new int[8];
                                player1.HandNames = new string[8];
                                player1.Health = 500;
                                player1.cardsdestroyed = 0;
                                player1.cardslost = 0;
                                player1.Mana = 2;
                                player1.ManaCount = 2;
                                player1.Ability_timer = 0;
                                player1.cardselectnumber = 0;
                                player1.placeanswer = "";
                                player1.IsWon = false;

                                player2.HandID = new int[8];
                                player2.HandNames = new string[8];
                                player2.Health = 500;
                                player2.cardsdestroyed = 0;
                                player2.cardslost = 0;
                                player2.Mana = 2;
                                player2.ManaCount = 2;
                                player2.Ability_timer = 0;
                                player2.cardselectnumber = 0;
                                player2.placeanswer = "";
                                player2.IsWon = false;

                                break;
                            
                            case "no":
                            case "No":
                                Messages.GlobalDisplay("Thank you for playing, goodbye.");
                                count = false;
                                PlayAgain = false;
                                break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("That's not a valid answer. Please try again");
                        count = true;
                    }
                }
            }
            Messages.GameOver();
            Environment.Exit(0);                        //Exiting the program if players don't want to play again.
        }
    }
}
