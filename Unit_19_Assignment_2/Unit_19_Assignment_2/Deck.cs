using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace Unit_19_Assignment_2
{
    public static class Deck
    {
        static bool choice;
        public static void Question(Player player)
        {
            
            if (player.LoggedIn == true)
            {
                bool Continue = false;
                while (Continue == false)
                {
                    Messages.Display(player.Name + ", would you like to update your deck? (yes/no):");          //Players that logged in get choice for updating deck or not
                    string answer = Messages.Input();
                    try
                    {
                        switch (answer)
                        {
                            case "Yes":
                                Messages.Message("Entering editing mode...");
                                Continue = true;
                                Update(player);
                                break;
                            case "yes":
                                Messages.Message("Entering editing mode...");
                                Continue = true;
                                Update(player);
                                break;
                            case "No":
                                Messages.Message("Okay, continuing...");
                                Continue = true;
                                break;
                            case "no":
                                Messages.Message("Okay, continuing...");
                                Continue = true;
                                break;
                        }
                    }
                    catch
                    {
                        Messages.Display("That's not a correct input, please try again.");
                        Continue = false;
                    }
                }//End of while loop
            }//End of If statement

            else if (player.LoggedIn == false)
            {
                Create(player);                                         //Players that signed up create a deck
            }
        }//End of Question() 
        public static void Create(Player player)
        {
            Messages.Message("Welcone to the Deck Creator!");
            Messages.Display("You currently have no deck");
            Messages.Display(" ");
            Messages.Display("Your deck consists of 35 spaces, but they don't all have to contain a card");
            Messages.Display(" ");
            Messages.Display("Possible cards from most powerful to least powerful are:");
            Messages.Display("Mage");
            Messages.Display("Elven Archer");
            Messages.Display("Orc");
            Messages.Display("Knight");
            Messages.Display("Ninja");
            Messages.Display("Archer");
            Messages.Display("Medic");
            Messages.Display("Soldier");
            Messages.Display("Peasant");
            Messages.Display(" ");
            Messages.Display("Alternatively, enter 'empty' to not enter a card into a position");
            Messages.Display(" ");

            int count = 0;
            while (count <= 34)
            {
                Messages.Display("Position" + count + " of 34 is empty.");
                Messages.Display(" ");
                Messages.Display("Mage");
                Messages.Display("Elven Archer");
                Messages.Display("Orc");
                Messages.Display("Knight");
                Messages.Display("Ninja");
                Messages.Display("Archer");
                Messages.Display("Medic");
                Messages.Display("Soldier");
                Messages.Display("Peasant");
                Messages.Display("empty");
                Messages.Display(" ");

                bool Continue = false;
                while (Continue == false)
                {
                    Messages.Display("What would you like to add?: ");                  //Choosing card to add to deck
                    string answer = Messages.Input();
                    if (answer == "Peasant" || answer == "Soldier" || answer == "Medic" || answer == "Archer" || answer == "Ninja" || answer == "Knight" || answer == "Orc" || answer == "Elven Archer" || answer == "Mage" || answer == "empty")
                    {
                        Messages.Display("Adding " + answer + " to your deck.");
                        player.DeckNames[count] = answer;
                        Continue = true;
                    }
                    else
                    {
                        Messages.Display("Invalid input, please try again.");
                        Continue = false;
                    }
                }
                count += 1;
            }
            Messages.Message("Deck successfully updated.");
            UpdateDatabase(player);
        }

        public static void Update(Player player)
        {
            int count = 0;
            while (count <= 34)
            {
                Messages.Display("Position " + count + " of 34 is: " + player.DeckNames[count]);
                bool Continue = false;
                while (Continue == false)
                {
                    Messages.Display("Would you like to 'keep', 'change' or 'remove' it?");             //Updating deck for logged in players that choose update
                    string answer = Messages.Input();
                    if (answer == "keep" || answer == "Keep")
                    {
                        Messages.Display("You have selected 'keep'.");
                        count += 1;
                        Continue = true;
                        answer = "";
                    }
                    else if (answer == "change" || answer == "Change")
                    {
                        Messages.Display("You have selected 'change'.");
                        bool changeloop = true;
                        while (changeloop == true)
                        {
                            Messages.Display("What would you like to change to? (please use a capital letter at the start of the name e.g Mage): ");
                            string changeanswer = Messages.Input();
                            if (changeanswer == "Peasant" || changeanswer == "Soldier" || changeanswer == "Medic" || changeanswer == "Archer" || changeanswer == "Ninja" || changeanswer == "Knight" || changeanswer == "Orc" || changeanswer == "Elven Archer" || changeanswer == "Mage")
                            {
                                player.DeckNames[count] = changeanswer;
                                Messages.Display("Card changed");
                                changeloop = false;
                            }
                            else
                            {
                                Messages.Display("Invalid card name, please try again.");
                                changeloop = true;
                            }
                        }
                        count += 1;
                        Continue = true;
                        answer = "";
                    }
                    else if (answer == "remove" || answer == "Remove")
                    {
                        Messages.Display("You have selected 'remove'.");
                        player.DeckNames[count] = "empty";
                        Messages.Display("Card removed");
                        count += 1;
                        Continue = true;
                        answer = "";
                    }
                    else
                    {
                        Messages.Display("Invalid input, please try again.");
                        Continue = false;
                        answer = "";
                    }
                }
            }
            Messages.Message("Deck successfully updated.");
            UpdateDatabase(player);
        }

        public static void UpdateDatabase(Player player)                        //Updating the deck of the player in the database for next time they log on
        {
            Messages.Display("Updating database");
            int count = 0;
            while (count <= 34)
            {
                string conn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\Users\\Luke\\Documents\\Unit_19_Assignment_2\\UsersDatabase - original.accdb";
                using (OleDbConnection connection = new OleDbConnection(conn))
                    try
                    {
                        //Console.WriteLine("Changing");
                        //Console.WriteLine(count + player.DeckNames[count] + player.TheUser);
                        string SQLString = @"UPDATE Users SET Card" + count + " = '" + player.DeckNames[count] + "' WHERE User = '" + player.TheUser + "'";
                        OleDbCommand command = new OleDbCommand(SQLString);
                        command.Connection = connection;
                        connection.Open();
                        command.ExecuteNonQuery();
                        count += 1;
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        Messages.Display(ex.Message);
                        Messages.Input();
                        connection.Close();
                        break;
                    }
            }
            Fill(player);
            Messages.Display("Database updated");
        }

        public static void Fill(Player player)                                                  //Filling the deck list with cards
        {
            Messages.Message("Filling deck...");
            player.deck = new List<Card>();
            player.CardsPlaced = new List<Card>();
            player.Hand = new List<Card>();
            int thecount = 0;
            while (thecount <= 34)
            {
                player.deck.Add(new Card(player.DeckNames[thecount]));
                player.deck[thecount].ID = thecount;
                thecount += 1;
            }
            Messages.Message("Deck filled!");
            
        }

    }//End of Deck Class
}
