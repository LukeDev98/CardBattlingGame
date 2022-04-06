using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Unit_19_Assignment_2
{
    public static class Connection
    {
        public static string hostName = Dns.GetHostName();                                                    // Retrive the Name of HOST  
        public static string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();                    // Get the IP  
        public static IPAddress ip = IPAddress.Parse(myIP);                                                   //Change IP from string to IPAddress
        public static TcpListener server = new TcpListener(ip, 8080);                                         //Create server
        public static TcpClient client = default(TcpClient);

        public static void ConnectionSend(string message)                   //Method for sending strings to client
        {
            int bytecount = Encoding.ASCII.GetByteCount(message);
            byte[] sendData = new byte[bytecount];
            sendData = Encoding.ASCII.GetBytes(message);
            NetworkStream stream = client.GetStream();
            stream.Write(sendData, 0, sendData.Length);
        }
        public static string ConnectionRecieve()                    //Method for recieving strings from client
        {
            byte[] recievedBuffer = new byte[150];
            NetworkStream stream = client.GetStream();
            stream.Read(recievedBuffer, 0, recievedBuffer.Length);
            string response = System.Text.Encoding.UTF8.GetString(recievedBuffer, 0, recievedBuffer.Length);
            return response;
        }
    }
}
