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
        private Vector3 _previousMousePosition;

        private struct InputDirectionJob : IJobForComponents<InputDirection>
        {
            public void Execute(in JobInfo jobInfo, in Ent ent, ref InputDirection component)
            {
            }
        }

        public void OnUpdate(ref SystemContext context)
        {
            float x = 0f;
            float z = 0f;

            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                z += 1f;
            }

            if (UnityEngine.Input.GetKey(KeyCode.S))
            {
                z -= 1f;
            }

            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                x -= 1f;
            }

            if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                x += 1f;
            }
            
            var direction = new float3(x, 0f, z);

            if (math.length(direction) > 0.1f)
            {
                //means we've got here input
                direction = math.normalize(direction);

                context.world.parent.SendNetworkEvent(new PlayerInputData
                {
                    Direction = direction,
                }, PlayerApplyInputDataSystem.DelegatePlayerInputData);
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