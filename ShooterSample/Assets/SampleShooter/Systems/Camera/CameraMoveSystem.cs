using ME.BECS;
using ME.BECS.Jobs;
using ME.BECS.Players;
using ME.BECS.Transforms;
using SampleShooter.Components.Camera;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace SampleShooter.Systems.Camera
{
    [BurstCompile]
    public struct CameraMoveSystem : IUpdate
    {

        [BurstCompile]
        public struct CameraMoveJob : IJobFor1Aspects1Components<TransformAspect, CameraComponent>
        {
            public float3 CurrentPlayerPosition;

            public void Execute(in JobInfo jobInfo, in Ent cameraEntity, ref TransformAspect cameraTransform,
                ref CameraComponent camera)
            {
                float3 positionOffset = cameraEntity.Read<CameraPositionOffsetComponent>().PositionOffset;
                float3 finalCameraPosition = CurrentPlayerPosition + positionOffset;
                cameraTransform.position = finalCameraPosition;
            }
        }

        public void OnUpdate(ref SystemContext context)
        {

            var playerPosition = new float3(0, 0, 0);

            JobHandle cameraMoveJob = context.Query().Schedule<CameraMoveJob, TransformAspect, CameraComponent>
            (new CameraMoveJob()
            {
                CurrentPlayerPosition = playerPosition,
            });

            context.SetDependency(cameraMoveJob);
        }
    }
}