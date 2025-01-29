using ME.BECS;
using ME.BECS.Players;
using ME.BECS.Transforms;
using SampleShooter.Components.Level;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace SampleShooter.Systems.Player
{
    public struct PlayerInitializeSystem : IAwake
    {
        public Config PlayerConfig;

        public void OnAwake(ref SystemContext context)
        {
            PlayerAspect playerAspect = PlayerUtils.GetActivePlayer();
            var playerEntity = Ent.New();
            playerAspect.ent = playerEntity;
            PlayerConfig.Apply(in playerEntity);
            playerEntity.GetOrCreateAspect<TransformAspect>();

            JobHandle jobHandle = API.Query(context)
                .WithAll<LevelComponent, LevelPlayerSpawnPointComponent>()
                .ParallelFor(64)
                .ForEach((in CommandBufferJob commandBuffer) =>
                {
                    Ent ent = commandBuffer.ent;
                    float3 playerSpawnPoint = ent.Read<LevelPlayerSpawnPointComponent>().PlayerSpawnPoint;
                    playerEntity.GetAspect<TransformAspect>().position = playerSpawnPoint;
                    Debug.Log("Player spawn point in PlayerInitializeSystem: " + playerSpawnPoint);
                });

            jobHandle.Complete();
        }
    }
}