# MQTT with ProtoBuf Example
A simple MQTT and Protobuf example in C#.

The idea was to prototype a solution where you are able to connect to a topic with a specific message type:

```csharp
client.Subscribe<string>("hello/world", (sender, topic, message) =>
{
    Console.WriteLine($"New Message: {message}");
});

client.Subscribe<Status>("game/status", (sender, topic, message) =>
{
    Console.WriteLine($"New position: {message.Position.X}, {message.Position.Y}");
});
```

And of course publish new messages (strings and proto atm):

```csharp
client.Publish("hello/world", "hello world");

var status = new Status
{
    Enabled = true,
    Position = new Vector3()
    {
        X = 0.5f, Y =  1.5f, Z = 2.4f
    }
};
client.Publish("hello/status", status);
```