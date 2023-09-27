using TransformSync;
using Unity.Entities;
using Unity.Physics.Aspects;
using Unity.Transforms;

namespace Util.Aggro
{
    public readonly partial struct AggroFinderAspect : IAspect
    {
        public readonly Entity Entity;

        public readonly RefRO<AggroFinderTag> Tag;
        public readonly TransformSyncAspect TransformSyncAspect;

        private readonly RefRW<AggroFinderData> _finderData;
        
        public Entity? Target
        {
            get => _finderData.ValueRO.TargetEntity;
            set => _finderData.ValueRW.TargetEntity = value;
        }

        public LocalTransform TargetTransform
        {
            get => _finderData.ValueRO.TargetTransform;
            set => _finderData.ValueRW.TargetTransform = value;
        }

        public float FindDistance => _finderData.ValueRO.FindDistance;
    }
}
