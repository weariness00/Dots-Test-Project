using AnimatorSync;
using Unity.Entities;
using UnityEngine;

namespace Monster.FSM
{
    public partial struct MonsterFSMPatrolJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        private static readonly int SpeedIndex = Animator.StringToHash("Speed"); 
        private void Execute(MonsterFsmAspect aspect, ref MonsterFsmPatrolData patrol, [ChunkIndexInQuery] int sortKey)
        {
            switch (patrol.Index)
            {
                case -1:
                    ECB.SetComponentEnabled<MonsterFsmPatrolData>(sortKey, aspect.Entity, false);
                    patrol.Index = 0;
                    break;
                case 0: // enter
                    patrol.Index = Enter(aspect, sortKey);
                    break;
                case 1: // update
                    patrol.Index = Update(aspect, sortKey);
                    break;
                case 2: // resume
                    break;
                case 3:
                    break;
                case 4: // Exit
                    patrol.Index = -1;
                    break;
            }
        }

        short Enter(MonsterFsmAspect aspect, int sortKey)
        {
            aspect.SetRandomPatrolPosition((uint)sortKey);
            
            var parameter = ECB.SetBuffer<AnimatorFloatHashes>(sortKey, aspect.Entity);
            parameter.Add(new AnimatorFloatHashes() { Hash = SpeedIndex, Value = 1 });
            return 1;
        }

        short Update(MonsterFsmAspect aspect, int sortKey)
        {
            aspect.Patrol(DeltaTime);
            return 1;
        }
    }
}
