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
            //because we are in logic world and  world.parent = SampleShooterLogicInitializer.Instance.world; (see SampleShooterLogicInitializer)
            World logicWorld = context.world.parent;
            //get players system from logic world
            var playersSystem = logicWorld.GetSystem<PlayersSystem>();
            //get active player aspect from players system
            PlayerAspect playerAspectFromSystem = playersSystem.GetActivePlayer();
            //get players entity from player aspect
            Ent playerEntity = playerAspectFromSystem.ent;
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