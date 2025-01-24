using ME.BECS;
using Unity.Mathematics;
using UnityEngine;

namespace SampleShooter.Components.Level
{
    public struct LevelComponent : IConfigComponent
    {
    }

    public struct LevelIdComponent : IConfigComponent
    {
        public int LevelId;
    }
    
    public struct LevelSpawnPointComponent : IConfigComponent
    {
        public float3 LevelSpawnPoint;
    }

    public struct LevelPlayerSpawnPointComponent : IConfigComponent
    {
        public float3 PlayerSpawnPoint;
    }
    
    public struct LevelEnemySpawnPointComponent : IConfigComponent
    {
        public float3 EnemySpawnPoint;
    }
}