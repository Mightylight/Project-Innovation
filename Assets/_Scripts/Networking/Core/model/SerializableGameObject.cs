using Networking.Core;
using UnityEngine;

namespace _Scripts
{
    public class SerializableGameObject : ASerializable
    {
        public Vector3 _transformPosition;
        public Quaternion _transformRotation;
        public int networkObjectID;

        public override void Serialize(Packet pPacket)
        {
            pPacket.Write(networkObjectID);
            pPacket.Write(_transformPosition.x);
            pPacket.Write(_transformPosition.y);
            pPacket.Write(_transformPosition.z);
            pPacket.Write(_transformRotation.x);
            pPacket.Write(_transformRotation.y);
            pPacket.Write(_transformRotation.z);
            pPacket.Write(_transformRotation.w);
        }

        public override void Deserialize(Packet pPacket)
        {
            networkObjectID = pPacket.ReadInt();
            _transformPosition.x = pPacket.ReadFloat();
            _transformPosition.y = pPacket.ReadFloat();
            _transformPosition.z = pPacket.ReadFloat();
            _transformRotation.x = pPacket.ReadFloat();
            _transformRotation.y = pPacket.ReadFloat();
            _transformRotation.z = pPacket.ReadFloat();
            _transformRotation.w = pPacket.ReadFloat();
        }
    }
}