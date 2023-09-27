using TransformSync;
using Unity.Entities;

namespace Util.Aggro
{
    public readonly partial struct AggroTargetAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly RefRO<AggroTargetTag> Tag;

        public readonly TransformSyncAspect TransformSyncAspect;
    }
}
