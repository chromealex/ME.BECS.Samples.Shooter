using ME.BECS;
using NativeTrees;
using ME.BECS.Transforms;
using SampleShooter.Components.Level;

namespace SampleShooter.Systems.Level
{
    public struct LevelInitializeSystem : IAwake
    {
        public Config LevelConfig;

        public void OnAwake(ref SystemContext context)
        {
            //this system is in logic graph
            //creating levelView entity
            World logicWorld = context.world;
            var levelViewEntity = Ent.New(in context);
            LevelConfig.Apply(in levelViewEntity);
            
            var tr = levelViewEntity.GetAspect<TransformAspect>();
            tr.position = levelViewEntity.Read<LevelSpawnPointComponent>().LevelSpawnPoint;
            
            //retrieving quad tree
            var quadTreeInsertSystem = logicWorld.GetSystem<QuadTreeInsertSystem>();
            int levelViewTreeIndex = quadTreeInsertSystem.AddTree();
            safe_ptr<NativeOctree<Ent>> levelViewQuadTree = quadTreeInsertSystem.GetTree(levelViewTreeIndex);
            
            //applying required components on levelViewEntity
            
            levelViewEntity.Set(new LevelQuadTreeComponent()
            {
                QuadTree = levelViewQuadTree
            });

            levelViewEntity.Set(new LevelQuadTreeIndexComponent()
            {
                QuadTreeIndex = levelViewTreeIndex
            });
        }
    }
}