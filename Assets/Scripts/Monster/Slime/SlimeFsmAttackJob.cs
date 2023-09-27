using AnimatorSync;
using Monster.FSM;
using Unity.Entities;
using UnityEngine;

namespace Monster.Slime
{
    public partial struct SlimeFsmAttackJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        private static readonly int AttackIndex = Animator.StringToHash("Attack"); 
        
        private void Execute(SlimeFsmAspect aspect,ref MonsterFsmAttackData attack ,[ChunkIndexInQuery] int sortKey)
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
                    attack.Index = Update(aspect, sortKey);
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
        
        short Enter(SlimeFsmAspect aspect, int sortKey)
        {
            aspect.AnimatorAspect.TriggerHashes.Add(new AnimatorTriggerHashes() { Hash = AttackIndex});
            return 1;
        }

        short Update(SlimeFsmAspect aspect, int sortKey)
        {
            ECB.SetComponentEnabled<MonsterFsmPatrolData>(sortKey, aspect.Entity, true);
            return 4;
        }
    }
}
