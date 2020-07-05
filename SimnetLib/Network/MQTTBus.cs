using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;

namespace SimnetLib.Network
{
    public class MQTTBus : IBus
    {
        private IMqttClient _mqttClient;

        public MQTTBus()
        {
            var factory = new MqttFactory();
            _mqttClient = factory.CreateMqttClient();
        }

        public async void Connect(IPAddress host, int port, string clientId)
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(host.ToString(), port)
                .WithClientId(clientId)
                .Build();

            var result = await _mqttClient.ConnectAsync(options, CancellationToken.None);

            if (result.ResultCode == MqttClientConnectResultCode.Success)
            {
                Console.WriteLine("mqtt: connected!");
                _mqttClient.UseApplicationMessageReceivedHandler(e =>
                {
                    MessageReceived?.Invoke(this, 
                        e.ApplicationMessage.Topic,
                        e.ApplicationMessage.Payload);
                });
            }
            else
            {
                Console.WriteLine("mqtt: could not connect");
            }
        }

        public async void Publish(string topic, byte[] payload, MessageAssurance assurance = MessageAssurance.UnReliable)
        {
            if (assurance == MessageAssurance.Reliable)
            {
                await Task.Run(() => _mqttClient.PublishAsync(
                    new MqttApplicationMessageBuilder()
                        .WithTopic(topic)
                        .WithPayload(payload)
                        .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.ExactlyOnce)
                        .Build()
                ));
            }
            else
            {
                await _mqttClient.PublishAsync(topic, payload);
            }
        }

        public async void Subscribe(string topic)
        {
            await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
        }

        public event MessageReceivedEventHandler MessageReceived;
    }
}