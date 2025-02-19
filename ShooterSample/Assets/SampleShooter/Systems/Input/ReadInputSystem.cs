using ME.BECS;
using ME.BECS.Jobs;
using ME.BECS.Network;
using ME.BECS.Network.Markers;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;
using SampleShooter.Data;
using SampleShooter.Systems.Player;

namespace SampleShooter.Systems.Input
{
    public struct ReadInputSystem : IUpdate, IDestroy
    {
        private float3 _currentDirection;
        private ClassPtr<UnityEngine.Camera> _camera;

        public void SetCamera(UnityEngine.Camera camera)
        {
            _camera = new ClassPtr<UnityEngine.Camera>(camera);
        }

        public void OnUpdate(ref SystemContext context)
        {
            float3 newDirection = float3.zero;
            bool directionChanged = false;

            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                newDirection.z += 1f;
            }
            if (UnityEngine.Input.GetKey(KeyCode.S))
            {
                newDirection.z -= 1f;
            }
            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                newDirection.x -= 1f;
            }
            if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                newDirection.x += 1f;
            }

            if (!math.all(newDirection == _currentDirection))
            {
                _currentDirection = newDirection;
                directionChanged = true;
            }

            if (directionChanged)
            {
                context.world.parent.SendNetworkEvent(new PlayerInputData()
                {
                    Direction = _currentDirection,
                }, PlayerApplyInputDataSystem.DelegatePlayerInputData);
            }
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                context.world.parent.SendNetworkEvent(new MousePositionData()
                {
                    MousePosition = UnityEngine.Input.mousePosition,
                }, PlayerShootSystem.DelegateMouseLeftClickData);
            }

            // context.world.parent.SendNetworkEvent(new MousePositionData()
            // {
            //     MousePosition =  UnityEngine.Input.mousePosition,
            // }, PlayerRotationToPointerSystem.DelegateMousePositionData);
            // return;

            // Vector3 currentMousePosition = UnityEngine.Input.mousePosition;
            //
            // if(math.abs((currentMousePosition - _previousMousePosition).sqrMagnitude) > 0.1f)
            // {
            //     _previousMousePosition = currentMousePosition;
            //     
            //     context.world.parent.SendNetworkEvent(new MousePositionData()
            //     {
            //         MousePosition = currentMousePosition,
            //     }, PlayerRotationToPointerSystem.DelegateMousePositionData);
            // }
        }

        public void OnDestroy(ref SystemContext context)
        {
            _camera.Dispose();
        }
    }
}