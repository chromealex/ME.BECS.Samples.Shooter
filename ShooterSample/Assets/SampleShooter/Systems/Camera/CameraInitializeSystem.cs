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
    public struct CameraInitializeSystem : IAwake
    {
        public Config CameraConfig;
        
        [BurstCompile]
        public struct CameraInitializeJob : IJobFor1Aspects1Components<TransformAspect, CameraComponent>
        {
            public void Execute(in JobInfo jobInfo, in Ent cameraEntity, ref TransformAspect cameraTransform,
                ref CameraComponent cameraComponent)
            {
                Ent playerEntity = PlayerUtils.GetActivePlayer().ent;

                if (!playerEntity.IsAlive())
                {
                    return;
                }

                float3 cameraPositionOffset = cameraEntity.Read<CameraPositionOffsetComponent>().PositionOffset;
                float3 playerPosition = playerEntity.GetAspect<TransformAspect>().position;
                float3 finalCameraPosition = playerPosition + cameraPositionOffset;
                cameraTransform.position = finalCameraPosition;
            }
        }
        
        public void OnAwake(ref SystemContext context)
        {
            var cameraEntity = Ent.New(in context);
            CameraConfig.Apply(in cameraEntity);
            cameraEntity.Set(new CameraRayComponent());

            JobHandle cameraInitializeJob = context.Query()
                .Schedule<CameraInitializeJob, TransformAspect, CameraComponent>();

            context.SetDependency(cameraInitializeJob);
        }
    }
}