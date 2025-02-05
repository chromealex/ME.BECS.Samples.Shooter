using ME.BECS;
using ME.BECS.Jobs;
using ME.BECS.Network;
using ME.BECS.Network.Markers;
using UnityEngine;
using Unity.Jobs;
using Unity.Mathematics;
using SampleShooter.Components.Input;
using SampleShooter.Data;
using SampleShooter.Initializers;
using SampleShooter.Systems.Player;

namespace SampleShooter.Systems.Input
{
    public struct ReadInputSystem : IUpdate
    {
        private float3 _currentDirection;

        public void OnUpdate(ref SystemContext context)
        {
            // float3 direction = float3.zero;
            //
            // if (UnityEngine.Input.GetKeyDown(KeyCode.W))
            // {
            //     direction.z += 1f;
            //     context.world.parent.SendNetworkEvent(new PlayerInputData()
            //     {
            //         Direction = direction,
            //     }, PlayerApplyInputDataSystem.DelegatePlayerInputData);
            // }
            // if (UnityEngine.Input.GetKeyUp(KeyCode.W))
            // {
            //     direction.z -= 1f;
            //     context.world.parent.SendNetworkEvent(new PlayerInputData()
            //     {
            //         Direction = direction,
            //     }, PlayerApplyInputDataSystem.DelegatePlayerInputData);
            // }
            //
            // if (UnityEngine.Input.GetKeyDown(KeyCode.A))
            // {
            //     direction.x -= 1f;
            //     context.world.parent.SendNetworkEvent(new PlayerInputData()
            //     {
            //         Direction = direction,
            //     }, PlayerApplyInputDataSystem.DelegatePlayerInputData);
            // }
            // if (UnityEngine.Input.GetKeyUp(KeyCode.A))
            // {
            //     direction.x += 1f;
            //     context.world.parent.SendNetworkEvent(new PlayerInputData()
            //     {
            //         Direction = direction,
            //     }, PlayerApplyInputDataSystem.DelegatePlayerInputData);
            // }
            //
            // if (UnityEngine.Input.GetKeyDown(KeyCode.S))
            // {
            //     direction.z -= -1f;
            //     context.world.parent.SendNetworkEvent(new PlayerInputData()
            //     {
            //         Direction = direction,
            //     }, PlayerApplyInputDataSystem.DelegatePlayerInputData);
            // }
            // if (UnityEngine.Input.GetKeyUp(KeyCode.S))
            // {
            //     direction.z += 1f;
            //     context.world.parent.SendNetworkEvent(new PlayerInputData()
            //     {
            //         Direction = direction,
            //     }, PlayerApplyInputDataSystem.DelegatePlayerInputData);
            // }
            //
            // if (UnityEngine.Input.GetKeyDown(KeyCode.D))
            // {
            //     direction.x += 1f;
            //     context.world.parent.SendNetworkEvent(new PlayerInputData()
            //     {
            //         Direction = direction,
            //     }, PlayerApplyInputDataSystem.DelegatePlayerInputData);
            // }
            // if (UnityEngine.Input.GetKeyUp(KeyCode.D))
            // {
            //     direction.x -= 1f;
            //     context.world.parent.SendNetworkEvent(new PlayerInputData()
            //     {
            //         Direction = direction,
            //     }, PlayerApplyInputDataSystem.DelegatePlayerInputData);
            // }
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
    }
}