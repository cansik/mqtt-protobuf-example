using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using ProtoBuf;
using SimnetLib.Network;

namespace SimnetLib
{
    public delegate void MessageEventHandler<T>(object sender, string topic, T message);
    
    public class SimnetClient
    {
        private INetworkBus _networkBus;
        private Dictionary<string, Subscription> _subscriptions;

        public string ClientId { get; private set; }

        public SimnetClient(INetworkBus networkBus, string clientId)
        {
            _networkBus = networkBus;
            _subscriptions = new Dictionary<string, Subscription>();
            ClientId = clientId;
        }

        public void Connect(IPAddress host, int port)
        {
            _networkBus.MessageReceived += NetworkBusOnMessageReceived;

            _networkBus.Connect(host, port, ClientId);
        }

        public void Subscribe<T>(string topic, MessageEventHandler<T> handler) where T : class
        {
            _subscriptions.Add(topic, new Subscription(typeof(T), handler));
            _networkBus.Subscribe(topic);
        }

        public void Publish<T>(string topic, T payload) where T : class
        {
            var data = new byte[0];
            
            // example, later use protobuf for serialisation
            if (typeof(T) == typeof(string))
            {
                data = Encoding.UTF8.GetBytes((string)(object)payload);
            }
            else if(typeof(T).IsProto())
            {
                // protobuf message
                using (var stream = new MemoryStream())
                {
                    Serializer.Serialize(stream, payload);
                    data = stream.ToArray();
                }
            }
            
            _networkBus.Publish(topic, data);
        }

        private void NetworkBusOnMessageReceived(object sender, string topic, byte[] payload)
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
            else if(subscription.Type.IsProto())
            {
                // protobuf message
                using (var stream = new MemoryStream(payload))
                {
                    value = Serializer.Deserialize(stream, Activator.CreateInstance(subscription.Type));
                }
            }
            
            handler?.DynamicInvoke(this, topic, value);
        }
    }
}