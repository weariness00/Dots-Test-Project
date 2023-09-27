using AnimatorSync;
using Monster.FSM;
using Unity.Entities;
using UnityEngine;

namespace Monster.Slime
{
    public partial struct SlimeFsmMoveTargetJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        private static readonly int SpeedIndex = Animator.StringToHash("Speed"); 

        private void Execute(SlimeFsmAspect aspect, ref MonsterFsmMoveTargetData moveTarget, [ChunkIndexInQuery]int sortKey)
        {
            switch (moveTarget.Index)
            {
                case -1:
                    ECB.SetComponentEnabled<MonsterFsmMoveTargetData>(sortKey, aspect.Entity, false);
                    moveTarget.Index = 0;
                    break;
                case 0: // enter
                    moveTarget.Index = Enter(aspect, sortKey);
                    break;
                case 1: // update
                    moveTarget.Index = Update(aspect, sortKey);
                    break;
                case 2: // resume
                    break;
                case 3:
                    break;
                case 4: // Exit
                    moveTarget.Index = -1;
                    break;
            }     
        }
        
        short Enter(SlimeFsmAspect aspect, int sortKey)
        {
            aspect.AnimatorAspect.FloatHashes.Add(new AnimatorFloatHashes() { Hash = SpeedIndex, Value = 2 });
            return 1;
        }

        short Update(SlimeFsmAspect aspect, int sortKey)
        {
            if (aspect.FsmAspect.TargetDistanceEary)
            {
                ECB.SetComponentEnabled<MonsterFsmAttackData>(sortKey, aspect.Entity, true);
                return 4;
            }
            
            aspect.FsmAspect.MoveTarget(aspect.FsmAspect.AggroFinderAspect.TargetTransform.Position, DeltaTime, false);
            return 1;
        }
    }
}
