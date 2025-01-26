using ME.BECS;
using UnityEngine;
using Unity.Jobs;
using SampleShooter.Components.Input;

namespace SampleShooter.Systems.Input
{
    public struct DestroyInputDirectionComponentSystem : IUpdate
    {
        public void OnUpdate(ref SystemContext context)
        {
            JobHandle jobHandle = API.Query(context)
            .With<InputDirection>()
            .ParallelFor(64)
            .ForEach((in CommandBufferJob commandBuffer) =>
            {
                Ent ent = commandBuffer.ent;
                var direction = ent.Read<InputDirection>().Direction;
                ent.Destroy();
                Debug.Log($"DestroyInputDirectionComponentSystem: destroyed entity with direction value: {direction}");
            });

            context.SetDependency(jobHandle);
        }
    }
}