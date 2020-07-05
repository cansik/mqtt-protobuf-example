using System;

namespace SimnetLib
{
    public class Subscription
    {
        public Subscription(Type type, Delegate @delegate)
        {
            Type = type;
            Delegate = @delegate;
        }

        public Type Type { get; private set; }
        public Delegate Delegate { get; private set; }
    }
}