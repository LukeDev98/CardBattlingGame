using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Unit_19_Assignment_2_Client
{
    class Program
    {
        public static int port;
        public static string serverIP;
        public static TcpClient client;
        static void Main(string[] args)
        {
            bool Connected = false;
            while (Connected == false)
            {
                try
                {
                    Console.WriteLine("Please enter the server IP address: ");              //Entering address of server and trying to connect
                    serverIP = Console.ReadLine();
                    Console.WriteLine("Please enter the server port: ");
                    port = Int32.Parse(Console.ReadLine());
                    client = new TcpClient(serverIP, port);
                    Connected = true;
                }
                catch
                {
                    Console.WriteLine("Incorrect IP address or port number, please try again");
                    Connected = false;
                }
            }
            Console.WriteLine("Connected to server!");
            bool RecieveLoop = true;
            while (RecieveLoop == true)                                 //Loop for recieving data
            {
                string message = RecieveResponse(client);



                if (message.Contains("..input.."))                         //Input command from server
                {
                    string reply = Console.ReadLine();
                    SendInput(client, reply);
                }



                else if (message.Contains("..message.."))          //Message command from server. Displays string in different format to ..Display..
                {
                    SendNull(client);
                    bool done = false;
                    int count = 0;
                    while (count <= 4)
                    {
                        string display = RecieveResponse(client);
                        
                        Console.WriteLine(display);
                        
                        SendNull(client);
                        count += 1;

                    }

                }


                else if (message.Contains("..display.."))              //Display command from server. Displays message
                {
                    SendNull(client);
                    string response = RecieveResponse(client);
                    Console.WriteLine(response);
                    SendNull(client);
                }
                else if (message.Contains("..GameOver.."))             //GameOver command from server. Ends game
                {
                    RecieveLoop = false;
                }
            }
            Environment.Exit(0);
        }

        public static void SendNull(TcpClient client)                   //Send back response with no content
        {
            int bytecount = Encoding.ASCII.GetByteCount(" ");
            byte[] sendData = new byte[bytecount];
            sendData = Encoding.ASCII.GetBytes(" ");
            NetworkStream stream = client.GetStream();
            stream.Write(sendData, 0, sendData.Length);
        }
        public static void SendInput(TcpClient client, string reply)            //Send input back
        {
            int bytecount = Encoding.ASCII.GetByteCount(reply);
            byte[] sendData = new byte[bytecount];
            sendData = Encoding.ASCII.GetBytes(reply);
            NetworkStream stream = client.GetStream();
            stream.Write(sendData, 0, sendData.Length);
        }
        public static string RecieveResponse(TcpClient client)              //Recieve response from server
        {
            byte[] recievedBuffer = new byte[150];
            NetworkStream stream = client.GetStream();
            stream.Read(recievedBuffer, 0, recievedBuffer.Length);
            string msg = System.Text.Encoding.UTF8.GetString(recievedBuffer, 0, recievedBuffer.Length);
            //string msg = "";
            //foreach (byte b in recievedBuffer)
            //{
            //    msg += Convert.ToString(b);
            //}
            ////string message = msg.ToString();
            //Console.WriteLine(msg);

            return msg;
        }
    }
}
