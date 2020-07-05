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

            var status = new Status
            {
                Enabled = true,
                Position = new Vector3()
                {
                    X = 0.5f, Y =  1.5f, Z = 2.4f
                }
            };
                
            //client.Publish("hello/world", "hello world");

            client.Publish("hello/status", status);

            Console.WriteLine("all done!");
        }
    }
}