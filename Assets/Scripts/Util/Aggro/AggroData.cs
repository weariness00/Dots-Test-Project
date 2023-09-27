using Unity.Entities;
using Unity.Transforms;

namespace Util.Aggro
{
    public struct AggroTargetTag : IComponentData{}
    public struct AggroFinderTag : IComponentData{}

    public struct AggroFinderData : IComponentData
    {
        public Entity? TargetEntity;
        public LocalTransform TargetTransform;
        public float FindDistance;
    }
}
