using System;
using System.Net;
using System.Threading;
using Simnet.Protocol;
using SimnetLib;
using SimnetLib.Network;

namespace FirstClient
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var bus = new MQTTBus();
            var client = new SimnetClient(bus, "FirstClient");
            client.Connect(IPAddress.Loopback, 1883);
            
            Thread.Sleep(1000);
            
            client.Subscribe<string>("hello/world", (sender, topic, message) =>
            {
                Console.WriteLine($"New Message: {message}");
            });

            client.Subscribe<Status>("hello/status", (sender, topic, message) =>
            {
                Console.WriteLine($"New position: {message.Position.X}, {message.Position.Y}");
            });

            Console.WriteLine("listening for messages!");
            Console.ReadKey(true);
        }
    }
}