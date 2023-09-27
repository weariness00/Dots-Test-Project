using AnimatorSync;
using Monster.FSM;
using TransformSync;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Monster
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct MonsterInitSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        
        }

        public void OnDestroy(ref SystemState state)
        {

        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (MO, entity) in SystemAPI.Query<MonsterObject>().WithEntityAccess())
            {
                var monsterObject = GameObject.Instantiate(MO.GameObject);
                ecb.AddComponent<MonsterFsmTag>(entity);
                
                ecb.AddComponent(entity, new TransformSyncData(){Transform = monsterObject.transform});
                ecb.AddComponent(entity, new AnimatorSyncData(){Animator = monsterObject.GetComponent<Animator>()});
                
                ecb.AddComponent<TransformSyncInitTag>(entity);

                ecb.AddComponent(entity, new MonsterFsmIdleData(){Index = 0});
                ecb.AddComponent(entity, new MonsterFsmPatrolData(){Index = -1});
                ecb.AddComponent(entity, new MonsterFsmMoveTargetData(){Index = -1});
                ecb.AddComponent(entity, new MonsterFsmAttackData(){Index = -1});

                ecb.AddComponent(entity, new PatrolPosition(){Position = float3.zero});
                
                ecb.RemoveComponent<MonsterObject>(entity);
            }
        }
    }
}
