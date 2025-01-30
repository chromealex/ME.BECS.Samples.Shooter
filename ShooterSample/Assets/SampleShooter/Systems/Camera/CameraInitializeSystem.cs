using ME.BECS;
using ME.BECS.Transforms;
using UnityEngine;

namespace SampleShooter.Systems.Camera
{
    public struct CameraInitializeSystem : IAwake
    {
        public Config CameraConfig;
        public void OnAwake(ref SystemContext context)
        {
            //simply create camera entity and apply camera config to it
            Debug.Log($"{nameof(CameraInitializeSystem)} started to work");
            var cameraEntity = Ent.New(in context);
            CameraConfig.Apply(in cameraEntity);
            cameraEntity.GetOrCreateAspect<TransformAspect>();
        }
    }
}