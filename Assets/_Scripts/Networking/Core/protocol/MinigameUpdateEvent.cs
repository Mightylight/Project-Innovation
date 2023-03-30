using _Scripts.Networking.Core.model;
using Networking.Core;

namespace _Scripts.Networking.Core.protocol
{
    public class MinigameUpdateEvent : ASerializable
    {
        
        MinigameData _data;

        public override void Serialize(Packet pPacket)
        {
            pPacket.Write(_data);
        }

        public override void Deserialize(Packet pPacket)
        {
            throw new System.NotImplementedException();
        }
    }
}