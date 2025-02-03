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
    public struct CameraRayCastSystem : IUpdate
    {
        [BurstCompile]
        public struct CameraRayCastJob : IJobForComponents<CameraComponent, CameraRayComponent>
        {
            public void Execute(in JobInfo jobInfo, in Ent ent, ref CameraComponent c0, ref CameraRayComponent cameraRayComponent)
            {
                var playerPosition = PlayerUtils.GetActivePlayer().ent.GetAspect<TransformAspect>().position;
                Ray cameraRay = cameraRayComponent.CameraRay;
                var playerPlane = new Plane(Vector3.up, playerPosition);

                if (playerPlane.Raycast(cameraRay, out float hitDistance))
                {
                    float3 raycastPoint = cameraRay.GetPoint(hitDistance);
                    PlayerUtils.GetActivePlayer().ent.GetAspect<TransformAspect>().forward = raycastPoint - playerPosition;
                }
            }
        }

        public void OnUpdate(ref SystemContext context)
        {
            JobHandle cameraRayCastJob = context
                .Query()
                .Schedule<CameraRayCastJob, CameraComponent, CameraRayComponent>();
            
            context.SetDependency(cameraRayCastJob);
        }
    }
}