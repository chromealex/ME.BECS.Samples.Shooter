using ME.BECS;
using ME.BECS.Jobs;
using ME.BECS.Network;
using ME.BECS.Network.Markers;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;
using SampleShooter.Components.Player;
using SampleShooter.Components.Input;
using SampleShooter.Initializers;

namespace SampleShooter.Systems.Input
{
    public struct ReadInputSystem : IUpdate
    {
        private struct InputDirectionJob : IJobForComponents<InputDirection>
        {
            public void Execute(in JobInfo jobInfo, in Ent ent, ref InputDirection component)
            {
            }
        }

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

            var direction = new float3(x, 0f, z);

            if (math.length(direction) > 0.1f)
            {
                
                // context.world.parent.SendNetworkEvent(new MoveUnitsData() {
                //     fromLength = (ushort)this.unitRequests.Count,
                //     from = arrFrom,
                //     to = nearestPoint.Read<PointIdComponent>().value,
                // }, MoveTo);
                
                direction = math.normalize(direction);
                //means we've got here input

                // NetworkModule networkModule = SampleShooterLogicInitializer.Instance.GetNetworkModule();
                // JobHandle jobHandle = API.Query(context)
                //     .With<NetworkModuleComponent>()
                //     .ParallelFor(64)
                //     .ForEach((in CommandBufferJob commandBuffer) =>
                //     {
                //         Ent networkModuleEntity = commandBuffer.ent;
                //         var networkModule = networkModuleEntity.Read<NetworkModuleComponent>().NetworkModule.Value;
                //         context.world.parent.SendNetworkEvent();
                //
                //     });

                Debug.Log($"{nameof(ReadInputSystem)} Created {nameof(InputDirection)} with value {direction}!");

                // jobHandle.Complete();
            }
        }
    }
}