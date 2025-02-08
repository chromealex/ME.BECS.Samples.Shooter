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
        public View BulletView;
        public Config BulletConfig;

        public void OnUpdate(ref SystemContext context)
        {
            JobHandle jobPlayerShooting = context
                .Query()
                .Schedule<JobPlayerShooting, TransformAspect, PlayerComponent, PlayerCanShootComponent>(
                    new JobPlayerShooting()
                    {
                        View = BulletView,
                        BulletConfig = BulletConfig
                    });

            context.SetDependency(jobPlayerShooting);
        }

        [BurstCompile]
        public struct
            JobPlayerShooting : IJobFor1Aspects2Components<TransformAspect, PlayerComponent, PlayerCanShootComponent>
        {
            public View View;
            public Config BulletConfig;

            public void Execute(in JobInfo jobInfo, in Ent playerEntity, ref TransformAspect playerTransform,
                ref PlayerComponent c0,
                ref PlayerCanShootComponent c1)
            {
                // var bulletEntity = Ent.New(in jobInfo, "New Bullet");
                // PlayerUtils.SetOwner(in bulletEntity, PlayerUtils.GetOwner(in playerEntity));
                // var tr = bulletEntity.GetOrCreateAspect<TransformAspect>();
                // tr.position = playerTransform.position;
                // tr.rotation = playerTransform.rotation;
                // var bullet = bulletEntity.GetOrCreateAspect<BulletAspect>();
                // bullet.component.sourceUnit = playerEntity;
                // bulletEntity.Set(new BulletRuntimeComponent());
                // bulletEntity.InstantiateView(View);
                //
                // playerEntity.Remove<PlayerCanShootComponent>();

                BulletUtils.CreateBullet(playerEntity, playerTransform.position, playerTransform.rotation, 0, default,
                    float3.zero, BulletConfig, View, 2.0f, jobInfo);
                playerEntity.Remove<PlayerCanShootComponent>();
            }
        }

        [BurstCompile]
        public struct JobPlayerShoot : IJobFor1Aspects1Components<TransformAspect, PlayerComponent>
        {
            public void Execute(in JobInfo jobInfo, in Ent playerEntity, ref TransformAspect playerTransform,
                ref PlayerComponent playerComponent)
            {
                if (!playerEntity.Has<PlayerCanShootComponent>())
                {
                    playerEntity.Set(new PlayerCanShootComponent
                    {
                        CanShoot = true,
                    });
                }
            }
        }


        [NetworkMethod]
        [AOT.MonoPInvokeCallback(typeof(NetworkMethodDelegate))]
        public static void DelegateMouseLeftClickData(in InputData data, ref SystemContext context)
        {
            Debug.Log($"Left mouse click");

            JobHandle jobHandle = context.Query().Schedule<JobPlayerShoot, TransformAspect, PlayerComponent>(
                new JobPlayerShoot()
                {
                });

            context.SetDependency(jobHandle);
        }
    }
}