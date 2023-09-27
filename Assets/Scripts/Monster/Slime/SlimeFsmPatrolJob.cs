using AnimatorSync;
using Monster.FSM;
using Unity.Entities;
using UnityEngine;

namespace Monster.Slime
{
    public partial struct SlimeFsmPatrolJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        private static readonly int SpeedIndex = Animator.StringToHash("Speed"); 
        private void Execute(SlimeFsmAspect aspect, ref MonsterFsmPatrolData patrol, [ChunkIndexInQuery] int sortKey)
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

        short Enter(SlimeFsmAspect aspect, int sortKey)
        {
            aspect.FsmAspect.SetRandomPatrolPosition((uint)sortKey);
            aspect.AnimatorAspect.FloatHashes.Add(new AnimatorFloatHashes() { Hash = SpeedIndex, Value = 1 });
            return 1;
        }

        short Update(SlimeFsmAspect aspect, int sortKey)
        {
            if (aspect.FsmAspect.FinderTarget)
            {
                ECB.SetComponentEnabled<MonsterFsmMoveTargetData>(sortKey, aspect.Entity, true);
                return 4;
            }

            aspect.FsmAspect.Patrol(DeltaTime);
            return 1;
        }
    }
}
