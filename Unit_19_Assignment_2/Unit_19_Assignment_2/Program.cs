using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace Unit_19_Assignment_2
{
    public class Program
    {
        public int banana = 0;
        static void Main(string[] args)
        {
            try
            {
                Connection.server.Start();                                  //Start the server so the client can connect
                Console.WriteLine("server started");
                Console.WriteLine("ip: " + Connection.ip.ToString() + "   port: " + 8080.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
                Environment.Exit(0);
            }

            while (true)
            {
                Connection.client = Connection.server.AcceptTcpClient();            //Wait for client connection
                Player player1 = new Player();                                  //Instantiate player objects
                Player player2 = new Player();
                Processes.Turns(player1, player2);                              //Run turns method if Processes static class
            }
        }
    }
}
