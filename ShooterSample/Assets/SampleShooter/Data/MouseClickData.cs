using ME.BECS;
using ME.BECS.Network;
using Unity.Mathematics;

namespace SampleShooter.Data
{
    public struct MouseClickData : IPackageData
    {
        public float3 MousePosition;
        
        public void Serialize(ref StreamBufferWriter writer)
        {
            writer.Write(MousePosition);
        }

        public void Deserialize(ref StreamBufferReader reader)
        {
            reader.Read(ref MousePosition);
        }
    }
}