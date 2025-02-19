using ME.BECS;
using ME.BECS.Bullets;
using ME.BECS.Jobs;
using ME.BECS.Transforms;
using SampleShooter.Components.Bullets;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace SampleShooter.Systems.Bullets
{
    [BurstCompile]
    public struct BulletsRaycastSystems : IUpdate
    {
        public void OnUpdate(ref SystemContext context)
        {
            JobHandle jobBulletsRaycast = context
                .Query()
                .AsParallel()
                .Schedule<JobBulletsRaycast,
                    BulletAspect,
                    TransformAspect,
                    QuadTreeQueryAspect,
                    BulletConfigComponent>(
                    new JobBulletsRaycast() { });
            context.SetDependency(jobBulletsRaycast);
        }

        [BurstCompile]
        public struct JobBulletsRaycast : IJobFor3Aspects1Components<BulletAspect, TransformAspect, QuadTreeQueryAspect,
            BulletConfigComponent>
        {
            public void Execute(in JobInfo jobInfo, in Ent ent, ref BulletAspect a0, ref TransformAspect a1,
                ref QuadTreeQueryAspect a2,
                ref BulletConfigComponent c0)
            {
                foreach (Ent target in a2.readResults.results)
                {
                    if (!target.IsAlive())
                    {
                        continue;
                    }
                    
                    //в QuadQueryAspect можно перенести все фильтры выборки объектов
                    //это делается на момент запроса квад дерева
                    // target.Read<>()
                }
            }

            // public void Execute(in JobInfo jobInfo, in Ent bulletEntity,
            //     ref BulletAspect bulletAspect,
            //     ref TransformAspect bulletTransform,
            //     ref BulletComponent bulletComponent,
            //     ref BulletConfigComponent bulletConfigComponent)
            // {
                // Debug.Log("Bullet raycast");
                //
                // var sphereRadius = 0.5f;
                // var maxDistance = 100.0f;
                // float3 direction = bulletTransform.forward;
                //
                // if (Physics.SphereCast(bulletTransform.position, sphereRadius, direction,
                //         out RaycastHit hit, maxDistance))
                // {
                //     Debug.Log("Bullet raycast found collision");
                // }
                //
                // Debug.DrawLine(bulletTransform.position, bulletTransform.position + direction * maxDistance, Color.red, 1.0f);
                // Debug.DrawRay(bulletTransform.position, direction * sphereRadius * 2.0f, Color.green, 1.0f);
            // }
        }
    }
}