using ME.BECS;
using ME.BECS.Transforms;
using UnityEngine;
using Unity.Collections;
using SampleShooter.Components.Level;
using Unity.Jobs;
using Unity.Mathematics;
using SampleShooter.Components.Player;
using SampleShooter.Components.Input;

namespace SampleShooter.Systems.Input
{
    public struct ReadInputSystem : IUpdate
    {
        public void OnUpdate(ref SystemContext context)
        {
            float x = 0f;
            float z = 0f;

            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                z += 1f;
            }
            if (UnityEngine.Input.GetKey(KeyCode.S))
            {
                z -= 1f;
            }
            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                x -= 1f;
            }
            if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                x += 1f;
            }

            float3 direction = new float3(x, 0f, z);

            if (math.length(direction) > 0.1f)
            {
                direction = math.normalize(direction);
                //means we've got here input

                JobHandle jobHandle = API.Query(context)
                .With<PlayerComponent>()
                .ParallelFor(64)
                .ForEach((in CommandBufferJob commandBuffer) =>
                {
                    Ent playerEntity = commandBuffer.ent;
                    playerEntity.Set(new InputDirection()
                    {
                        Direction = direction,
                    });
                });

                Debug.Log($"{nameof(ReadInputSystem)} Created {nameof(InputDirection)} with value {direction}!");


                jobHandle.Complete();
            }
        }
    }
}