using System;
using System.Linq;
using ProtoBuf;

namespace SimnetLib
{
    public static class ProtoExtensions
    {
        public static bool IsProto(this Type type)
        {
            return type.GetInterfaces().Any(i => i == typeof(IExtensible));
        }
    }
}