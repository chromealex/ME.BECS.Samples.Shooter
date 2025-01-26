using ME.BECS;
using ME.BECS.Jobs;
using ME.BECS.Transforms;
using Unity.Mathematics;
using UnityEngine;
using Unity.Jobs;
using SampleShooter.Components.Input;
using Unity.Burst;
using SampleShooter.Components.Player;

[BurstCompile]
public struct PlayerMoveDirectionSystem : IUpdate
{
    public void OnUpdate(ref SystemContext context)
    {

        JobHandle playerMoveJob = context.Query().Schedule<JobPlayerMove, TransformAspect, InputDirection, PlayerComponent>
            (new JobPlayerMove()
            {
                dt = context.deltaTime,
            });


        context.SetDependency(playerMoveJob);
    }

    [BurstCompile]
    public struct JobPlayerMove : IJobFor1Aspects2Components<TransformAspect, InputDirection, PlayerComponent>
    {
        public float dt;

        public void Execute(in JobInfo jobInfo, in Ent ent, ref TransformAspect playerTransform, ref InputDirection inputDirection, ref PlayerComponent _)
        {
            var direction = inputDirection.Direction;
            playerTransform.position += 10.0f * dt * direction;
            Debug.Log($"{nameof(PlayerMoveDirectionSystem)} move !");
            ent.Remove<InputDirection>();

        }
    }
}
