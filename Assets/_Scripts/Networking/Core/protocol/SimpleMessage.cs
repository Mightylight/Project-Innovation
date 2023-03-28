using Networking.Core;

namespace _Scripts.Networking.Core.protocol
{
    public class SimpleMessage : ASerializable
    {
        public string message;

        public override void Serialize(Packet pPacket)
        {
            pPacket.Write(message);
        }

        public override void Deserialize(Packet pPacket)
        {
            message = pPacket.ReadString();
        }
    }
}