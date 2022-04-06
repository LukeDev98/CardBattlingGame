using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Unit_19_Assignment_2
{
    public static class Messages                //Class holds methods for different types of messages
    {
        public static void GameOver()                           //Tell client to end game
        {
            Connection.ConnectionSend("..GameOver..");
        }
        public static string Input()                            //get input on server or client
        {
            string response;
            if (Processes.turn == 1)
            {
                response = Console.ReadLine();
                return response;
            }
            else 
            {
                Connection.ConnectionSend("..input..");
                response = Connection.ConnectionRecieve();
                return response;
            }
        }

        public static void Display(string message)                          //Display message on server OR client
        {
            if (Processes.turn == 1)
            {
                Console.WriteLine(message);                                       
            }
            else if (Processes.turn == 2)
            {
                Connection.ConnectionSend("..display..");
                string confirm = Connection.ConnectionRecieve();
                Connection.ConnectionSend(message);
                confirm = Connection.ConnectionRecieve();
            }
        }

        public static void GlobalDisplay(string message)
        {
            Console.WriteLine(message);                                 //Display message on server AND client
            Connection.ConnectionSend("..display..");
            string confirm = Connection.ConnectionRecieve();
            Connection.ConnectionSend(message);
            confirm = Connection.ConnectionRecieve();
        }

        public static void Message(string message)
        {
            string[] messagearray = new string[6];
            messagearray[0] = "---------------------------------------------------------------------------------------------";
            messagearray[1] = "=============================================================================================";
            messagearray[2] = message;
            messagearray[3] = "=============================================================================================";
            messagearray[4] = "---------------------------------------------------------------------------------------------";
            messagearray[5] = "";

            if (Processes.turn == 1)                                                            //Display Message
            {
                Console.WriteLine(messagearray[0]);                                                                 
                Console.WriteLine(messagearray[1]);                                                         
                Console.WriteLine(messagearray[2]);
                Console.WriteLine(messagearray[3]);
                Console.WriteLine(messagearray[4]);
                Console.WriteLine(messagearray[5]);
            }
            //if turn = 1;
            
            else if (Processes.turn == 2)                                                       //Send Message
            {
                Connection.ConnectionSend("..message..");
                string confirm = Connection.ConnectionRecieve();
                int count = 0;
                while (count <= 4)
                {
                    Connection.ConnectionSend(messagearray[count]);
                    confirm = Connection.ConnectionRecieve();
                    count += 1;
                }
                
            }

            //elif turn = 2;
            //SEND TO CLIENT
        }
        public static void GlobalMessage(string message)                                               //Same as above, but displays message for server and client at the same time.
        {
            string[] messagearray = new string[6];
            messagearray[0] = "---------------------------------------------------------------------------------------------";
            messagearray[1] = "=============================================================================================";
            messagearray[2] = message;
            messagearray[3] = "=============================================================================================";
            messagearray[4] = "---------------------------------------------------------------------------------------------";
            messagearray[5] = "";

            int count = 0;
            while (count <= 5)
            {
                Console.WriteLine(messagearray[count]);

            }

            Connection.ConnectionSend("..message..");
            string confirm = Connection.ConnectionRecieve();
            count = 0;
            while (count <= 5)
            {
                Connection.ConnectionSend(messagearray[count]);
                confirm = Connection.ConnectionRecieve();
                count += 1;
            }
            Connection.ConnectionSend("..done..");


            //SEND TO CLIENT
        }
    }
}
