using Unity.Entities;

namespace TransformSync
{
    public readonly partial struct TransformSyncInitAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly RefRW<TransformSyncInitTag> Tag;
    }
}
