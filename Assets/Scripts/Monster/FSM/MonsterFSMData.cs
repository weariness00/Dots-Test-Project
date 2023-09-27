using Unity.Entities;
using Unity.Mathematics;

namespace Monster.FSM
{
    public struct MonsterFsmTag : IComponentData{}

    public struct PatrolPosition : IComponentData
    {
        public float3 Position;
    }
    
    public struct MonsterFsmIdleData : IComponentData, IEnableableComponent
    {
        public short Index;
    }

    public struct MonsterFsmPatrolData : IComponentData, IEnableableComponent
    {
        public short Index;
    }

    public struct MonsterFsmMoveTargetData : IComponentData, IEnableableComponent
    {
        public short Index;
    }
    
    public struct MonsterFsmAttackData : IComponentData, IEnableableComponent
    {
        public short Index;
    }
}
