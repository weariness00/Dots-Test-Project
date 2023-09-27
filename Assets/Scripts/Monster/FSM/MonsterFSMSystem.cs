using AnimatorSync;
using Unity.Burst;
using Unity.Entities;

namespace Monster.FSM
{
    public partial struct MonsterFSMSystem : ISystem
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
            // var ecb= SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
            // var deltaTime = SystemAPI.Time.DeltaTime;
            //
            // var idle = new MonsterFSMIdleJob()
            // {
            //     DeltaTime = deltaTime,
            //     ECB = ecb.AsParallelWriter(),
            // };
            //
            // var patrol = new MonsterFSMPatrolJob()
            // {
            //     DeltaTime = deltaTime,
            //     ECB = ecb.AsParallelWriter(),
            // };
            //
            // var attack = new MonsterFSMAttackJob()
            // {
            //     DeltaTime = deltaTime,
            //     ECB = ecb.AsParallelWriter(),
            // };
            //
            // state.Dependency = idle.ScheduleParallel(state.Dependency);
            // state.Dependency = patrol.ScheduleParallel(state.Dependency);
            // state.Dependency = attack.ScheduleParallel(state.Dependency);
        }
    }
}
