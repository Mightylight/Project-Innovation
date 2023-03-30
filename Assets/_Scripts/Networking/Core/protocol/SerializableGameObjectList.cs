using System.Collections.Generic;
using Networking.Core;

namespace _Scripts.Networking.protocol
{
    public class SerializableGameObjectList : ASerializable
    {

        public List<SerializableGameObject> gameObjects;

        public override void Serialize(Packet pPacket)
        {
            pPacket.Write(gameObjects.Count);
            foreach (SerializableGameObject gameObject in gameObjects)
            {
                pPacket.Write(gameObject);
            }
        }

        public override void Deserialize(Packet pPacket)
        {
            int count = pPacket.ReadInt();
            gameObjects = new List<SerializableGameObject>();
            for (int i = 0; i < count; i++)
            {
                gameObjects.Add(pPacket.Read<SerializableGameObject>());
            }
        }
    }
}