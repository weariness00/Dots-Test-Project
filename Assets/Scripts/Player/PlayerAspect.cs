using AnimatorSync;
using TransformSync;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Player
{
    public readonly partial struct PlayerAspect : IAspect
    {
        public readonly Entity Entity;

        public readonly RefRO<PlayerTag> Tag;
        public readonly TransformSync.TransformSyncAspect TransformSyncAspect;
    }
}
