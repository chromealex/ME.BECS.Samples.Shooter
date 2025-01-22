using ME.BECS;
using ME.BECS.Transforms;
using Unity.Burst;
using UnityEngine;
using SampleShooter.Components.Level;

namespace SampleShooter.Systems.Level
{
    [BurstCompile]
    public struct LevelInitializeSystem : IAwake
    {
        public Config LevelConfig;
        public void OnAwake(ref SystemContext context)
        {
            //Create entity
            var levelEntity = Ent.New(in context);
            levelEntity.Set(new LevelComponent());
            //Applying all config components
            LevelConfig.Apply(in levelEntity);
            //Create transform aspect
            var tr = levelEntity.GetOrCreateAspect<TransformAspect>();
            tr.position = levelEntity.Get<LevelStartPositionComponent>().StartPosition;
            int levelId = levelEntity.Get<LevelIdComponent>().LevelId;
            int bugsAmount = levelEntity.Get<LevelBugsAmountComponent>().LevelBugsAmount;
            
            Debug.Log($"{nameof(LevelInitializeSystem)} finished work");
            Debug.Log($"Level id - {levelId}");
            Debug.Log($"Level bugsAmount - {bugsAmount}");
        }
    }
}