using ME.BECS;
using SampleShooter.Enums;
using Unity.Mathematics;
using UnityEngine;

namespace SampleShooter.Components.Camera
{
    public struct CameraComponent : IConfigComponent
    {
    }

    public struct CameraPositionOffsetComponent : IConfigComponent
    {
        public float3 PositionOffset;
    }

    public struct CameraRotationOffsetComponent : IConfigComponent
    {
        public float3 RotationOffset;
    }

    public struct CameraTypeComponent : IConfigComponent
    {
        public GameCameraType CameraType;
    }
    
    public struct CameraSmoothTimeComponent : IConfigComponent
    {
        public float SmoothTime;
    }

    public struct CameraVelocityComponent : IComponent
    {
        public float3 Velocity;
    }
    
    public struct CameraFollowSpeedComponent : IConfigComponent
    {
        public float FollowSpeed;
    }
    
    public struct CameraRayComponent : IComponent
    {
        public Ray CameraRay;
    }
}