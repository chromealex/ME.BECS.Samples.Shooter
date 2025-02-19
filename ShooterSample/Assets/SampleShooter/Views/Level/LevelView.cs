using ME.BECS;
using ME.BECS.Transforms;
using UnityEngine;
using NativeTrees;
using ME.BECS.Views;
using SampleShooter.Components.Level;

namespace SampleShooter.Views.Level
{
    public class LevelView : EntityView
    {
        [SerializeField] private GameObject[] _levelObstacles;
        
        protected override void OnInitialize(in EntRO entRO)
        {
            Debug.Log("On initialize level view!");
            World world = ent.World;
            Ent levelViewEntity = entRO.GetEntity();
            
            int levelViewQuadTreeIndex = levelViewEntity.Read<LevelQuadTreeIndexComponent>().QuadTreeIndex;
            safe_ptr<NativeOctree<Ent>> levelQuadTree = levelViewEntity.Read<LevelQuadTreeComponent>().QuadTree;

            foreach (GameObject obstacleGo in _levelObstacles)
            {
                //for every obstacle create entity
                Ent obstacleEntity = world.NewEnt();
                //every obstacle entity should have QuadTreeAspect
                var levelViewObstacleQuadTreeAspect = obstacleEntity.GetOrCreateAspect<QuadTreeAspect>();
                var levelViewObstacleTransformAspect = obstacleEntity.GetOrCreateAspect<TransformAspect>();
                
                levelViewObstacleTransformAspect.position = obstacleGo.transform.position;
                levelViewObstacleQuadTreeAspect.treeIndex = levelViewQuadTreeIndex;
                
                levelViewObstacleQuadTreeAspect.quadTreeElement = new QuadTreeElement()
                {
                    ignoreY = false,
                    treeIndex = levelViewQuadTreeIndex,
                    radius = obstacleGo.transform.localScale.x * 0.5f,
                };
            }
        }
    }
}