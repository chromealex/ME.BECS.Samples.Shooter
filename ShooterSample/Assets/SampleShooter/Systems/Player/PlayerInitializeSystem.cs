using ME.BECS;
using ME.BECS.Players;
using ME.BECS.Transforms;
using SampleShooter.Components.Level;
using SampleShooter.Components.Player;
using Unity.Jobs;
using UnityEngine;
using Unity.Mathematics;

namespace SampleShooter.Systems.Player
{
    public struct PlayerInitializeSystem : IAwake
    {
   
        public Config PlayerConfig;
        
        public void OnAwake(ref SystemContext context)
        {
            
            //здесь я должен взять массив игроков из Сервиса подбора игроков 
            // массив Вида PlayerData[]
            // foreach (var player in playersData)
            // {
            // }
            
            World logicWorld = context.world;
            var playersSystem = logicWorld.GetSystem<PlayersSystem>();
            PlayerAspect playerAspectFromSystem = playersSystem.GetActivePlayer();
            Ent playerEntity = playerAspectFromSystem.ent;

            int playerQuadTreeIndex = logicWorld.GetSystem<QuadTreeInsertSystem>().AddTree();//каждый игрок должен иметь свое дерево
            //сделать компонент PlayersTreeIndexComponent
            playerEntity.Set(new PlayersQuadTreeIndexComponent()
            {
                QuadTreeIndex = playerQuadTreeIndex
            });
            //по этому индексу нужно делать запросы
            
            //добавить QuadTreeAspect
            var playerQuadTreeAspect = playerEntity.GetOrCreateAspect<QuadTreeAspect>();
            playerQuadTreeAspect.treeIndex = playerQuadTreeIndex; //передали сюда индекс дерева который зарегестрирован на игроке

            var playerQuadTreeQueryAspect = playerEntity.GetOrCreateAspect<QuadTreeQueryAspect>();
            playerQuadTreeQueryAspect.query.treeMask = playerAspectFromSystem.readUnitsOthersTreeMask;
            playerQuadTreeQueryAspect.query.nearestCount = 0;//0 означает = все рядом с объектом
            playerQuadTreeQueryAspect.query.rangeSqr = 5f; // 
            
            
            
            playerEntity.Set(new PlayerMoveDirectionComponent()
            {
                MoveDirection = float3.zero,
            });
            
            
            PlayerConfig.Apply(in playerEntity);
            playerEntity.Set(new OwnerComponent()
            {
                ent = playerEntity
            });
            
            PlayerUtils.SetActivePlayer(playerAspectFromSystem);// устанавливаю за кого я играю
            //этот метод вызывается 1 раз на старте 
            

            JobHandle jobHandle = API.Query(context)
                .WithAll<LevelComponent, LevelPlayerSpawnPointComponent>()
                .ParallelFor(64)
                .ForEach((in CommandBufferJob commandBuffer) =>
                {
                    Ent ent = commandBuffer.ent;
                    float3 playerSpawnPoint = ent.Read<LevelPlayerSpawnPointComponent>().PlayerSpawnPoint;
                    playerEntity.GetAspect<TransformAspect>().position = playerSpawnPoint;
                    Debug.Log("Player spawn point in PlayerInitializeSystem: " + playerSpawnPoint);
                });
            //
            

            jobHandle.Complete();
        }
    }
}