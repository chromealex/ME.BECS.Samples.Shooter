using ME.BECS;
using ME.BECS.Bullets;
using ME.BECS.Jobs;
using ME.BECS.Network;
using ME.BECS.Players;
using ME.BECS.Transforms;
using ME.BECS.Views;
using SampleShooter.Components.Player;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace SampleShooter.Systems.Player
{
    [BurstCompile]
    public struct PlayerShootSystem : IUpdate
    {
        public View BulletEffectView;
        public Config BulletConfig;

        public void OnUpdate(ref SystemContext context)
        {
            JobHandle jobPlayerShooting = context
                .Query()
                .Schedule<JobPlayerShooting, 
                    TransformAspect, 
                    PlayerComponent, 
                    PlayerCanShootComponent,
                    PlayerMuzzlePositionComponent,
                    PlayerShootingRangeComponent>(
                    new JobPlayerShooting()
                    {
                        BulletEffectView = BulletEffectView,
                        BulletConfig = BulletConfig
                    });

            context.SetDependency(jobPlayerShooting);
        }

        [BurstCompile]
        public struct
            JobPlayerShooting : IJobFor1Aspects4Components<TransformAspect, PlayerComponent, PlayerCanShootComponent,
            PlayerMuzzlePositionComponent,PlayerShootingRangeComponent>
        {
            //this is the bullet effect view (Muzzle flash)
            public View BulletEffectView;

            //bullet config with 
            public Config BulletConfig;

            public void Execute(in JobInfo jobInfo, in Ent playerEntity,
                ref TransformAspect playerTransform,
                ref PlayerComponent playerComponent,
                ref PlayerCanShootComponent playerCanShootComponent,
                ref PlayerMuzzlePositionComponent playerMuzzlePositionComponent,
                ref PlayerShootingRangeComponent playerShootingRangeComponent)
            {
                // Get the muzzle point position
                float3 muzzlePosition = playerTransform.position + playerMuzzlePositionComponent.MuzzlePointOffset;
                float3 direction = math.forward(playerTransform.rotation);
                float shootingRange = playerShootingRangeComponent.ShootingRange;
                float3 targetPosition = muzzlePosition + direction * shootingRange;

                BulletUtils.CreateBullet(playerEntity,
                    muzzlePosition,
                    playerTransform.rotation,
                    0,
                    Ent.Null,
                    targetPosition,
                    BulletConfig,
                    BulletEffectView,
                    2.0f,
                    jobInfo);
                playerEntity.Remove<PlayerCanShootComponent>();
            }
        }

        [BurstCompile]
        public struct JobPlayerShoot : IJobForAspects<TransformAspect, PlayerAspect>
        {
            public void Execute(in JobInfo jobInfo, in Ent ent, ref TransformAspect playerTransform,
                ref PlayerAspect playerAspect)
            {
                var playerEntity = playerAspect.ent;
                var playerId = playerAspect.readIndex;//так можно получить индекс игрока
                
                if (!playerEntity.Has<PlayerCanShootComponent>())
                {
                    playerEntity.Set(new PlayerCanShootComponent
                    {
                        CanShoot = true,
                    });
                }
            }
        }

        //Отдать в IJob на обработку
        //this code can be counted as part logic system
        [NetworkMethod]
        [AOT.MonoPInvokeCallback(typeof(NetworkMethodDelegate))]
        public static void DelegateMouseLeftClickData(in InputData data, ref SystemContext context)
        {
            Debug.Log($"Left mouse click");

            var playerId= data.PlayerId;
            PlayerAspect activeShooterPlayerEntity = data.world.GetSystem<PlayersSystem>().GetPlayerEntity(playerId);
            //Create common job for 1 player
            context.dependsOn.Complete();
            // обрати внимание на то что конкретный игрок по ID 
            
            // JobHandle jobHandle = context.Query().Schedule<JobPlayerShoot, TransformAspect, PlayerComponent>(
            //     new JobPlayerShoot()
            //     {
            //     });
            //
            // context.SetDependency(jobHandle);
        }
    }
}