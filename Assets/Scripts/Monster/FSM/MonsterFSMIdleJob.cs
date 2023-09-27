using AnimatorSync;
using Unity.Entities;
using UnityEngine;

namespace Monster.FSM
{
    public partial struct MonsterFSMIdleJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        private static readonly int SpeedIndex = Animator.StringToHash("Speed"); 
    
        private void Execute(MonsterFsmAspect aspect, ref MonsterFsmIdleData idle, [ChunkIndexInQuery]int sortKey)
        {
            switch (idle.Index)
            {
                case -1:
                    ECB.SetComponentEnabled<MonsterFsmIdleData>(sortKey, aspect.Entity, false);
                    idle.Index = 0;
                    break;
                case 0: // enter
                    idle.Index = Enter(aspect, sortKey);
                    break;
                case 1: // update
                    idle.Index = Update(aspect, sortKey);
                    break;
                case 2: // resume
                    break;
                case 3:
                    break;
                case 4: // Exit
                    idle.Index = -1;
                    break;
            }     
        }

        short Enter(MonsterFsmAspect aspect, int sortKey)
        {
            // var parameter = ECB.SetBuffer<AnimatorFloatHashes>(sortKey, aspect.Entity);
            // parameter.Add(new AnimatorFloatHashes() { Hash = SpeedIndex, Value = 0 });
            aspect.AnimatorAspect.FloatHashes.Add(new AnimatorFloatHashes() { Hash = SpeedIndex, Value = 0 });
            return 1;
        }

        short Update(MonsterFsmAspect aspect, int sortKey)
        {
            ECB.SetComponentEnabled<MonsterFsmPatrolData>(sortKey, aspect.Entity, true);
            return 4;
        }
    }
}
