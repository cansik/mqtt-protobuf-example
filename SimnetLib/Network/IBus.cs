using System;
using System.Net;

namespace SimnetLib.Network
{
    public delegate void MessageReceivedEventHandler(object sender, string topic, byte[] payload);
    
    public interface IBus
    {
        void Connect(IPAddress host, int port, string clientId);
        void Publish(string topic, byte[] payload, MessageAssurance assurance = MessageAssurance.UnReliable);
        void Subscribe(string topic);

        event MessageReceivedEventHandler MessageReceived;
    }
}