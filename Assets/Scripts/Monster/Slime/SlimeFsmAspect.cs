using AnimatorSync;
using Monster.FSM;
using Unity.Entities;

namespace Monster.Slime
{
    public readonly partial struct SlimeFsmAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly MonsterFsmAspect FsmAspect;
        public readonly AnimatorSyncAspect AnimatorAspect;

        private readonly RefRO<SlimeTag> _tag;
    }
}
