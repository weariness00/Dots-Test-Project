using Unity.Burst;
using Unity.Entities;

namespace Monster.Slime
{
    public partial struct SlimeFsmSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<EndInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb= SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            var parallelEcb = ecb.AsParallelWriter();
            var deltaTime = SystemAPI.Time.DeltaTime;

            var idleJob = new SlimeFsmIdleJob()
            {
                ECB = parallelEcb,
                DeltaTime = deltaTime,
            };

            var patrolJob = new SlimeFsmPatrolJob()
            {
                ECB = parallelEcb,
                DeltaTime = deltaTime,
            };

            var moveTargetJob = new SlimeFsmMoveTargetJob()
            {
                ECB = parallelEcb,
                DeltaTime = deltaTime,
            };

            var attackJob = new SlimeFsmAttackJob()
            {
                ECB = parallelEcb,
                DeltaTime = deltaTime,
            };

            state.Dependency = idleJob.ScheduleParallel(state.Dependency);
            state.Dependency = patrolJob.ScheduleParallel(state.Dependency);
            state.Dependency = moveTargetJob.ScheduleParallel(state.Dependency);
            state.Dependency = attackJob.ScheduleParallel(state.Dependency);
        }
    }
}
