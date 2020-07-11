using System;
using System.Net;

namespace SimnetLib.Network
{
    public delegate void MessageReceivedEventHandler(object sender, string topic, byte[] payload);
    
    public interface INetworkBus
    {
        // todo: add more connect possibilities (with pw / cert usw.)
        
        void Connect(IPAddress host, int port, string clientId);
        
        // todo: add retain flag for publish
        // todo: add remove-retain support (retain with empty payload)

        void Publish(string topic, byte[] payload, MessageAssurance assurance = MessageAssurance.UnReliable);
        void Subscribe(string topic);

        event MessageReceivedEventHandler MessageReceived;
    }
}