// <auto-generated>
//   This file was generated by a tool; you should avoid making direct changes.
//   Consider using 'partial classes' to extend these types
//   Input: Simnet.proto
// </auto-generated>

#region Designer generated code
#pragma warning disable CS0612, CS0618, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
namespace Simnet.Protocol
{

    [global::ProtoBuf.ProtoContract()]
    public partial class Vector3 : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"x")]
        public float X { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"y")]
        public float Y { get; set; }

        [global::ProtoBuf.ProtoMember(3, Name = @"z")]
        public float Z { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class Status : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, Name = @"enabled")]
        public bool Enabled { get; set; }

        [global::ProtoBuf.ProtoMember(2, Name = @"position")]
        public Vector3 Position { get; set; }

    }

}

#pragma warning restore CS0612, CS0618, CS1591, CS3021, IDE1006, RCS1036, RCS1057, RCS1085, RCS1192
#endregion
