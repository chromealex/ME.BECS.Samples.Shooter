using ME.BECS;
using ME.BECS.Network;
using Unity.Mathematics;

namespace SampleShooter.Data
{
    public struct PlayerInputData : IPackageData
    {
        public float3 Direction;
        
        public void Serialize(ref StreamBufferWriter writer)
        {
            writer.Write(Direction);
        }

        public void Deserialize(ref StreamBufferReader reader)
        {
            reader.Read(ref Direction);
        }
    }
}