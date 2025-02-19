using ME.BECS;
using ME.BECS.Jobs;
using ME.BECS.Network;
using ME.BECS.Players;
using ME.BECS.Transforms;
using SampleShooter.Components.Camera;
using SampleShooter.Data;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace SampleShooter.Systems.Player
{
    [BurstCompile]
    public struct PlayerRotationToPointerSystem : IUpdate
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
            //пример последовательных Job 
         context
                .Query()
                .Schedule<CameraRayCastJob, CameraComponent, CameraRayComponent>()
                .AddDependency(ref context);
         
            context.SetDependency(context.Apply(context.dependsOn));//
            
            context
                .Query()
                .Schedule<CameraRayCastJob, CameraComponent, CameraRayComponent>()
                .AddDependency(ref context);
            
            // context.SetDependency(cameraRayCastJob);
        }
        
        [NetworkMethod]
        [AOT.MonoPInvokeCallback(typeof(NetworkMethodDelegate))]
        public static void DelegateMousePositionData(in InputData data, ref SystemContext context)
        {
            var mousePositionData = data.GetData<MousePositionData>();
        }
    }
}