using System;
using System.Net;
using System.Threading;
using SimnetLib;
using SimnetLib.Network;

namespace SecondClient
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var bus = new MQTTBus();
            var client = new SimnetClient(bus, "SecondClient");
            
            client.Connect(IPAddress.Loopback, 1883);
            
            Thread.Sleep(1000);
            
            client.Publish("hello/world", "hello world");

            Console.WriteLine("all done!");
        }
    }
}