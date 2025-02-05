using ME.BECS;
using ME.BECS.Network;
using SampleShooter.Data;
using Unity.Burst;
using UnityEngine;

namespace SampleShooter.Systems.Player
{
    [BurstCompile]
    public struct PlayerShootSystem
    {
        [BurstCompile]
        public struct JobPlayerAddShootComponent
        {
            
        }
        
        
        [NetworkMethod]
        [AOT.MonoPInvokeCallback(typeof(NetworkMethodDelegate))]
        public static void DelegateMouseLeftClickData(in InputData data, ref SystemContext context)
        {
            // context.Query().Schedule<JobPlayerMove, TransformAspect, PlayerComponent>
            // (new JobPlayerMove()
            // {
            //     dt = context.deltaTime,
            //     direction = playerInputData.Direction,
            // });
            //
            // context.SetDependency(playerMoveJob);
            
            // var mouseClickData = data.GetData<MouseClickData>();
            Debug.Log($"Left mouse click");
        }
    }
}