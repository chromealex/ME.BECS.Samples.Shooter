using ME.BECS;
using Unity.Mathematics;

namespace SampleShooter.Components.Player
{
    public struct PlayerMoveSpeedComponent : IConfigComponent
    {
        public float MoveSpeed;
    }
    
    public struct PlayerMoveDirectionComponent : IConfigComponent
    {
        public float3 MoveDirection;
    }
    
    public struct PlayerCanShootComponent : IComponent
    {
        public bool CanShoot;
    }
}