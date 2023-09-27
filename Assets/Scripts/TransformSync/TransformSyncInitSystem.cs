using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace TransformSync
{
    public partial struct TransformSyncInitSystem : ISystem
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

            var initJob = new TransformSyncInitJob()
            {
                ECB = ecb.AsParallelWriter(),
            };

            state.Dependency = initJob.ScheduleParallel(state.Dependency);
        }
    }
    
    public partial struct TransformSyncInitJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ECB;
        
        private void Execute(TransformSyncInitAspect aspect, [ChunkIndexInQuery]int sortKey)
        {
            ECB.AddComponent(sortKey, aspect.Entity, new TransformRotateData(){Rotate = float3.zero});
            
            ECB.AddComponent<TransformSyncTag>(sortKey, aspect.Entity);
            ECB.RemoveComponent<TransformSyncInitTag>(sortKey, aspect.Entity);
        }
    }
}
