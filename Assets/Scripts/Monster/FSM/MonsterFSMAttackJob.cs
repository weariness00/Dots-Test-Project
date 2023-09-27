using AnimatorSync;
using Unity.Entities;
using UnityEngine;

namespace Monster.FSM
{
    public partial struct MonsterFSMAttackJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        private static readonly int AttackIndex = Animator.StringToHash("Attack"); 

        private void Execute(MonsterFsmAspect aspect, ref MonsterFsmAttackData attack, [ChunkIndexInQuery] int sortKey)
        {
            switch (attack.Index)
            {
                case -1:
                    ECB.SetComponentEnabled<MonsterFsmAttackData>(sortKey, aspect.Entity, false);
                    attack.Index = 0;
                    break;
                case 0: // enter
                    attack.Index = Enter(aspect, sortKey);
                    break;
                case 1: // update
                    break;
                case 2: // resume
                    break;
                case 3:
                    break;
                case 4: // Exit
                    attack.Index = -1;
                    break;
            }
            
        }

        short Enter(MonsterFsmAspect aspect, int sortKey)
        {
            var trigger = ECB.SetBuffer<AnimatorTriggerHashes>(sortKey, aspect.Entity);
            trigger.Add(new AnimatorTriggerHashes() { Hash = AttackIndex });
            
            ECB.SetComponentEnabled<MonsterFsmIdleData>(sortKey, aspect.Entity, true);
            return 4;
        }
    }
}
 