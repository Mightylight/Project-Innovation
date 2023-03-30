using Networking.Core;

namespace _Scripts.Networking.Core.model
{
    public abstract class MinigameData : ASerializable
    {
        MinigameProgress _progress;
        MinigameType _minigame;
        public override void Serialize(Packet pPacket)
        {
            pPacket.Write((int)_progress);
            pPacket.Write((int)_minigame);
        }

        public override void Deserialize(Packet pPacket)
        {
            _progress = (MinigameProgress)pPacket.ReadInt();
            _minigame = (MinigameType)pPacket.ReadInt();
        }
    }
}