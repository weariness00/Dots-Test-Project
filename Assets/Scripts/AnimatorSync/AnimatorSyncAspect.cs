using Unity.Entities;

namespace AnimatorSync
{
    public readonly partial struct AnimatorSyncAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly DynamicBuffer<AnimatorTriggerHashes> TriggerHashes;
        public readonly DynamicBuffer<AnimatorBoolHashes> BoolHashes;
        public readonly DynamicBuffer<AnimatorFloatHashes> FloatHashes;
        public readonly DynamicBuffer<AnimatorIntHashes> IntHashes;
    }
}
