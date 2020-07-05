using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using SimnetLib.Network;

namespace SimnetLib
{
    public delegate void MessageEventHandler<T>(object sender, string topic, T message);
    
    public class SimnetClient
    {
        private IBus _bus;
        private Dictionary<string, Subscription> _subscriptions;

        public string ClientId { get; private set; }

        public SimnetClient(IBus bus, string clientId)
        {
            _bus = bus;
            _subscriptions = new Dictionary<string, Subscription>();
            ClientId = clientId;
        }

        public void Connect(IPAddress host, int port)
        {
            _bus.MessageReceived += BusOnMessageReceived;

            _bus.Connect(host, port, ClientId);
        }

        public void Subscribe<T>(string topic, MessageEventHandler<T> handler) where T : class
        {
            _subscriptions.Add(topic, new Subscription(typeof(T), handler));
            _bus.Subscribe(topic);
        }

        public void Publish<T>(string topic, T payload) where T : class
        {
            var data = new byte[0];
            
            // example, later use protobuf for serialisation
            if (typeof(T) == typeof(string))
            {
                data = Encoding.UTF8.GetBytes((string)(object)payload);
            }
            
            _bus.Publish(topic, data);
        }

        private void BusOnMessageReceived(object sender, string topic, byte[] payload)
        {
            if (!_subscriptions.ContainsKey(topic)) return;

            var subscription = _subscriptions[topic];
            var handler = subscription.Delegate;

            // example, later use protobuf for serialisation
            object value = null;
            if (subscription.Type == typeof(string))
            {
                value = Encoding.UTF8.GetString(payload);
            }
            
            handler?.DynamicInvoke(this, topic, value);
        }
    }
}