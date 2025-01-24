using ME.BECS;
using ME.BECS.Transforms;
using UnityEngine;
using SampleShooter.Components.Level;

namespace SampleShooter.Systems.Level
{
    public struct LevelInitSystem : IAwake
    {
        public Config LevelConfig;
        
        public void OnAwake(ref SystemContext context)
        {
            Debug.Log("LevelInitializeSystem started to work");
            var levelEntity = Ent.New(in context);
            LevelConfig.Apply(in levelEntity);
            
            var tr = levelEntity.GetOrCreateAspect<TransformAspect>();
            tr.position = levelEntity.Read<LevelSpawnPointComponent>().LevelSpawnPoint;
        }
    }
}