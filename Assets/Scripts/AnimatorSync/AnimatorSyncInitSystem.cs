using Unity.Burst;
using Unity.Entities;

namespace AnimatorSync
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct AnimatorSyncInitSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (tag, entity) in SystemAPI.Query<AnimatorSyncPropertyInitTag>().WithEntityAccess())
            {
                ecb.AddBuffer<AnimatorTriggerHashes>(entity);
                ecb.AddBuffer<AnimatorBoolHashes>(entity);
                ecb.AddBuffer<AnimatorFloatHashes>(entity);
                ecb.AddBuffer<AnimatorIntHashes>(entity);
                
                ecb.AddComponent<AnimatorSyncTag>(entity);
                ecb.RemoveComponent<AnimatorSyncPropertyInitTag>(entity);
            }
        }
    }
}
