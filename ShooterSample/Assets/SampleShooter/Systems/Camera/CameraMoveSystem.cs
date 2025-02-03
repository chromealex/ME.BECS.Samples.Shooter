using ME.BECS;
using ME.BECS.Jobs;
using ME.BECS.Players;
using ME.BECS.Transforms;
using SampleShooter.Components.Camera;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;

namespace SampleShooter.Systems.Camera
{
    [BurstCompile]
    public struct CameraMoveSystem : IUpdate
    {
        [BurstCompile]
        public struct CameraMoveJob : IJobFor1Aspects1Components<TransformAspect, CameraComponent>
        {
            public float dT;

            public void Execute(in JobInfo jobInfo, in Ent cameraEntity, ref TransformAspect cameraTransform,
                ref CameraComponent cameraComponent)
            {
                Ent playerEntity = PlayerUtils.GetActivePlayer().ent;

                if (!playerEntity.IsAlive())
                {
                    return;
                }

                float3 cameraPositionOffset = cameraEntity.Read<CameraPositionOffsetComponent>().PositionOffset;
                float cameraFollowSpeed = cameraEntity.Read<CameraFollowSpeedComponent>().FollowSpeed;

                float3 playerPosition = playerEntity.GetAspect<TransformAspect>().position;
                float3 finalCameraPosition = playerPosition + cameraPositionOffset;
                float3 currentCameraPos = cameraEntity.GetAspect<TransformAspect>().position;
                cameraTransform.position = math.lerp(currentCameraPos, finalCameraPosition, cameraFollowSpeed * dT);
            }
        }

        public void OnUpdate(ref SystemContext context)
        {
            JobHandle cameraMoveJob = context.Query().Schedule<CameraMoveJob, TransformAspect, CameraComponent>(
                new CameraMoveJob
                {
                    dT = context.deltaTime,
                });

            context.SetDependency(cameraMoveJob);
        }
    }
}