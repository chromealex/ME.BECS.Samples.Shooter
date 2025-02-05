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
        [BurstCompile]
        public struct JobPlayerMoveDirection : IJobFor1Aspects2Components<TransformAspect, PlayerComponent,
            PlayerMoveDirectionComponent>
        {
            public float dt;
            public float3 direction;

            public void Execute(in JobInfo jobInfo, in Ent playerEntity, 
                ref TransformAspect playerTransform, 
                ref PlayerComponent playerComponent,
                ref PlayerMoveDirectionComponent playerMoveDirectionComponent)
            {
                float moveSpeed = playerEntity.Read<PlayerMoveSpeedComponent>().MoveSpeed;
                playerTransform.position += dt * playerMoveDirectionComponent.MoveDirection * moveSpeed;
            }
        }
        
        [BurstCompile]
        public struct JobPlayerMove : IJobFor1Aspects2Components<TransformAspect, PlayerComponent,
            PlayerMoveDirectionComponent>
        {
            public float DT;
            public float3 Direction;

            public void Execute(in JobInfo jobInfo, in Ent playerEntity, ref TransformAspect playerTransform, ref PlayerComponent playerComponent,
                ref PlayerMoveDirectionComponent playerMoveDirectionComponent)
            {
                playerMoveDirectionComponent.MoveDirection = Direction;
            }
        }

        [NetworkMethod]
        [AOT.MonoPInvokeCallback(typeof(NetworkMethodDelegate))]
        public static void DelegatePlayerInputData(in InputData data, ref SystemContext context)
        {
            var playerInputData = data.GetData<PlayerInputData>();
            Debug.Log($"playerInputData");

            JobHandle playerMoveJob = context.Query()
                .Schedule<JobPlayerMove, TransformAspect, PlayerComponent, PlayerMoveDirectionComponent>
                (new JobPlayerMove()
                {
                    Direction = playerInputData.Direction,
                });

            context.SetDependency(playerMoveJob);
        }

        public void OnUpdate(ref SystemContext context)
        {
            JobHandle jobPlayerMoveDirection = context
                .Query()
                .Schedule<JobPlayerMoveDirection, TransformAspect, PlayerComponent, PlayerMoveDirectionComponent>(new JobPlayerMoveDirection()
                {
                    dt = context.deltaTime,
                });
            
            context.SetDependency(jobPlayerMoveDirection);
        }
    }
}