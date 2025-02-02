using ME.BECS;
using ME.BECS.Players;
using ME.BECS.Transforms;
using SampleShooter.Components.Level;
using Unity.Jobs;
using UnityEngine;
using Unity.Mathematics;

namespace SampleShooter.Systems.Player
{
    public struct PlayerInitializeSystem : IAwake
    {
        public Config PlayerConfig;
        
        public void OnAwake(ref SystemContext context)
        {
            World logicWorld = context.world.parent;
            var playersSystem = logicWorld.GetSystem<PlayersSystem>();
            PlayerAspect playerAspectFromSystem = playersSystem.GetActivePlayer();
            Ent playerEntity = playerAspectFromSystem.ent;
            PlayerConfig.Apply(in playerEntity);
            
            PlayerUtils.SetActivePlayer(playerAspectFromSystem);

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