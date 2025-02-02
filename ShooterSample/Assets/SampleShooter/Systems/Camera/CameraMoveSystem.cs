using ME.BECS;
using ME.BECS.Jobs;
using ME.BECS.Players;
using ME.BECS.Transforms;
using SampleShooter.Components.Camera;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace SampleShooter.Systems.Camera
{
    [BurstCompile]
    public struct CameraMoveSystem : IUpdate
    {
        [BurstCompile]
        public struct CameraMoveJob : IJobFor1Aspects2Components<TransformAspect, CameraComponent, CameraPositionOffsetComponent>
        {
            public void Execute(in JobInfo jobInfo, in Ent cameraEntity, ref TransformAspect cameraTransform, ref CameraComponent c0,
                ref CameraPositionOffsetComponent positionOffset)
            {
                Ent playerEntity = PlayerUtils.GetActivePlayer().ent;

                if (!playerEntity.IsAlive())
                {
                    return;
                }

                float3 playerPosition = playerEntity.GetAspect<TransformAspect>().position;
                float3 cameraPosOffset = positionOffset.PositionOffset;
                float3 finalCameraPosition = playerPosition + cameraPosOffset;
                cameraEntity.GetAspect<TransformAspect>().position = finalCameraPosition;
                cameraTransform.position = finalCameraPosition;
            }
        }

        public void OnUpdate(ref SystemContext context)
        {
            JobHandle cameraMoveJob = context.Query().Schedule<CameraMoveJob, TransformAspect, CameraComponent, CameraPositionOffsetComponent>
                (new CameraMoveJob() { });
            context.SetDependency(cameraMoveJob);
        }
    }
}