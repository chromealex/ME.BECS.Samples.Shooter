using ME.BECS;
using NativeTrees;
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

    public struct PlayerMuzzlePositionComponent : IConfigComponent
    {
        public float3 MuzzlePointOffset;
    }
    
    public struct PlayerShootingRangeComponent : IConfigComponent
    {
        public float ShootingRange;
    }

    public struct PlayersQuadTreeIndexComponent : IComponent
    {
        public int QuadTreeIndex;
    }
    
    public struct PlayerQuadTreeComponent : IComponent
    {
        public safe_ptr<NativeOctree<Ent>> QuadTree;
    }
}