using ME.BECS;
using ME.BECS.Transforms;
using UnityEngine;
using Unity.Collections;
using SampleShooter.Components.Level;
using Unity.Jobs;
using Unity.Mathematics;

public struct PlayerInitializeSystem : IAwake
{
    public Config PlayerConfig;

    public void OnAwake(ref SystemContext context)
    {
        var playerEntity = Ent.New(in context);
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