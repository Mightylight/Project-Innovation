using Networking.Core;

namespace _Scripts.Networking.Core.model
{
    public class LighterMinigameData : MinigameData
    {
        private bool isBurning;
        
        public override void Serialize(Packet pPacket)
        {
            base.Serialize(pPacket);
            pPacket.Write(isBurning);   
        }

        public override void Deserialize(Packet pPacket)
        {
            base.Deserialize(pPacket);
            isBurning = pPacket.ReadBool();
        }
    }
}