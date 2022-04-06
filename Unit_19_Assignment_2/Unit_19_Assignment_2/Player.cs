using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Net;
using System.Net.Sockets;

namespace Unit_19_Assignment_2
{
    public class Player : Display
    {
        public bool LoggedIn = false;
        public int MenuCount;
        public string Name;
        public string[] DeckNames = new string[35];
        public List<Card> deck;
        public List<Card> Hand;
        public List<Card> CardsPlaced;
        public int[] HandID = new int[8];
        public string[] HandNames = new string[8];
        public int Health = 500;
        public int cardsdestroyed = 0;
        public int cardslost = 0;
        public int Mana = 2;
        public int ManaCount = 2;
        public int Ability_timer;
        public string TheUser = "";
        public int cardselectnumber;
        public string placeanswer;
        public Card targetcard;
        public bool IsWon = false;
        public bool choice = false;

        //public bool LoggedIn = false;
        public void PlayerDisplay()
        {
            DisplayInformation(Name, Health, cardsdestroyed, cardslost, Mana, Ability_timer, "null");               //Display info on player
        }
        public void Menu()
        {
            Messages.Message("Welcome to the Fantasy Card Game!");
            Messages.Display("Would you like to log in or sign up?");
            bool Continue = false;
            while (Continue == false)
            {
                try
                {
                    Messages.Display("Enter 1 to log in, enter 2 to sign up");
                    string Input = Messages.Input();
                    int Answer;
                    Int32.TryParse(Input, out Answer);

                    if (Answer == 1)
                    {
                        Messages.Message("You have selected log in");
                        Login();
                        Continue = true;

                    }
                    else if (Answer == 2)
                    {
                        Messages.Message("You have selected sign up");
                        Signup();
                        Continue = true;

                    }
                }
                catch
                {
                    Messages.Message("That's not a valid input, please try again.");
                    Continue = false;
                }
            }

        }
        public void Login()                         //Users with pre-existing accounts on database can log in
        {
            //Messages Play = new Messages();
            bool Continue = false;
            while (Continue == false)
            {
                Messages.Display("Please enter your username and password");

                Messages.Display("Username: ");
                Messages.Display(" ");
                TheUser = Messages.Input();
                Messages.Display(" ");
                Messages.Display("Password: ");
                Messages.Display(" ");
                string ThePass = Messages.Input();
                Messages.Display(" ");

                try
                {
                    string conn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=C:\\Users\\Luke\\Documents\\Unit_19_Assignment_2\\UsersDatabase - original.accdb";
                    using (OleDbConnection connection = new OleDbConnection(conn))

                        try
                        {
                            //string sqlString = @"INSERT INTO Users (Username, [Password]) VALUES('" + theusername + "','" + thepassword + "')";
                            string SQLString = @"SELECT * FROM Users WHERE User = '" + TheUser + "' AND Pass = '" + ThePass + "'";

                            OleDbCommand command = new OleDbCommand(SQLString);

                            command.Connection = connection;
                            try
                            {
                                connection.Open();

                                command.ExecuteNonQuery();

                                connection.Close();
                                Console.WriteLine("Success");
                                int count = 0;
                                int cardcount = 0;
                                Messages.Display("Hello " + TheUser + ". Here is your saved deck:");
                                while (count <= 34)
                                {
                                    string SQLString2 = @"SELECT Card" + cardcount + " FROM Users WHERE User = '" + TheUser + "'";          //Checks to see if user's details match with database
                                    OleDbCommand command2 = new OleDbCommand(SQLString2);
                                    command2.Connection = connection;
                                    connection.Open();
                                    command2.ExecuteNonQuery();
                                    OleDbDataReader reader = command.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        //Console.WriteLine(reader.GetString(count));
                                        string placeholder = reader.GetString(cardcount + 2);                           //Loads saved deck from database
                                        DeckNames[cardcount] = placeholder;
                                        Messages.Display(placeholder);
                                    }
                                    reader.NextResult();
                                    cardcount += 1;
                                    count += 1;
                                    connection.Close();
                                }
                                Name = TheUser;
                                Continue = true;
                            }
                            catch (Exception ex)
                            {
                                Messages.Display(ex.Message);
                                string enter = Messages.Input();
                                Continue = false;
                            }
                        }
                        catch
                        {
                            Messages.Message("The username or password you have entered is incorrect, please try again.");
                            Continue = false;
                            Messages.Input();
                        }
                }
                catch
                {
                    Messages.Message("Could not connect to the database, please contact tech support");
                    Continue = false;
                }
            }//while loop ends
            //playerdeck.Question();
            LoggedIn = true;
            //Deck.Question();   
        }//login ends

        public void Signup()                            //New players can create an account
        {
            //Messages Play = new Messages();
            bool Continue = false;

            string ThePass = "";
            while (Continue == false)
            {
                bool Continue2 = false;
                while (Continue2 == false)
                {
                    try
                    {
                        Messages.Message("Please enter a username. This will be what you are referred to in the game.");
                        Messages.Display(" ");
                        Messages.Display("Username: ");
                        Messages.Display(" ");
                        TheUser = Messages.Input();
                        Messages.Display(" ");
                        Messages.Display("Password: ");
                        Messages.Display(" ");
                        ThePass = Messages.Input();
                        Messages.Display(" ");
                        Messages.Display("Are you sure this what you want? (yes/no)");
                        Messages.Display(" ");
                        string answer = Messages.Input();
                        Messages.Display(" ");

                        if (answer == "yes")
                        {
                            Continue2 = true;
                        }
                        else if (answer == "Yes")
                        {
                            Continue2 = true;
                        }
                        else if (answer == "no")
                        {
                            Continue2 = false;
                        }
                        else if (answer == "No")
                        {
                            Continue2 = false;
                        }
                    }
                    catch
                    {
                        Messages.Display("That answer isn't valid, please try again.");
                        Continue2 = false;
                    }

                }

                string conn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=..\\Unit_19_Assignment_2\\UsersDatabase - original.accdb";
                using (OleDbConnection connection = new OleDbConnection(conn))

                    try
                    {
                        string SQLString = @"INSERT INTO Users([User], Pass) VALUES('" + TheUser + "','" + ThePass + "')";
                        OleDbCommand command = new OleDbCommand(SQLString);

                        command.Connection = connection;
                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            // connection.Close();
                            int count = 0;
                            int cardcount = 0;
                            while (count <= 34)
                            {
                                string card = "Card";
                                card = card += cardcount;
                                string SQLString2 = @"UPDATE Users SET " + card + " = 'empty' WHERE User = '" + TheUser + "'";                      //Creates empty deck                           
                                OleDbCommand command2 = new OleDbCommand(SQLString2);
                                command2.Connection = connection;
                                //connection.Open();
                                command2.ExecuteNonQuery();
                                cardcount += 1;
                                count += 1;

                            }
                            Messages.Display("Success");
                            connection.Close();
                            Continue = true;
                            Name = TheUser;

                        }
                        catch (Exception ex)
                        {
                            Messages.Display(ex.Message);
                            string enter = Messages.Input();
                            Continue = false;
                        }
                    }
                    catch
                    {
                        Messages.Message("Could not connect to database, please try again.");
                        Continue = false;
                        string enter = Messages.Input();
                    }
                Name = TheUser;
            }//End of first while loop
             //playerdeck.Question();

            LoggedIn = false;
            //Deck.Question();
        }//End of Signup      

    }
}
