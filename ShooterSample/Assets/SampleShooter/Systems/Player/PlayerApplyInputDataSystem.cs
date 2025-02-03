using ME.BECS;
using ME.BECS.Jobs;
using ME.BECS.Network;
using ME.BECS.Players;
using ME.BECS.Transforms;
using SampleShooter.Components.Player;
using SampleShooter.Data;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace SampleShooter.Systems.Player
{
    [BurstCompile]
    public struct PlayerApplyInputDataSystem : IUpdate
    {
        public void OnUpdate(ref SystemContext context)
        {
            //i need to somehow get the input data
            //and apply it to the player
        }

        [BurstCompile]
        public struct JobPlayerMove : IJobFor1Aspects1Components<TransformAspect, PlayerComponent>
        {
            public float dt;
            public float3 direction;

            public void Execute(in JobInfo jobInfo, in Ent ent, ref TransformAspect playerTransform, ref PlayerComponent playerComponent)
            {
                float moveSpeed = ent.Read<PlayerMoveSpeedComponent>().MoveSpeed;
                playerTransform.position += dt * direction * moveSpeed;
            }
        }

        [NetworkMethod]
        [AOT.MonoPInvokeCallback(typeof(NetworkMethodDelegate))]
        public static void DelegatePlayerInputData(in InputData data, ref SystemContext context)
        {
            var playerInputData = data.GetData<PlayerInputData>();
            
            JobHandle playerMoveJob = context.Query().Schedule<JobPlayerMove, TransformAspect, PlayerComponent>
            (new JobPlayerMove()
            {
                dt = context.deltaTime,
                direction = playerInputData.Direction,
            });
            
            context.SetDependency(playerMoveJob);
        }
    }
}