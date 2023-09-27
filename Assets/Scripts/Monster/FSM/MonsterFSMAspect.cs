using AnimatorSync;
using TransformSync;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Util.Aggro;
using Random = Unity.Mathematics.Random;

namespace Monster.FSM
{
    public readonly partial struct MonsterFsmAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly RefRO<MonsterFsmTag> Tag;
        public readonly TransformSyncAspect Transform;
        public readonly AnimatorSyncAspect AnimatorAspect;
        public readonly AggroFinderAspect AggroFinderAspect;

        public readonly RefRW<MeshInfo> MeshInfo;
        public readonly RefRW<PatrolPosition> PatrolPosition;
        public void SetRandomPatrolPosition(uint seed)
        {
            float minV = -100f;
            float maxV = 100f;
            PatrolPosition.ValueRW.Position = Random.CreateFromIndex(seed)
                .NextFloat3(new float3(minV, 0f, minV), new float3(maxV, 0f, maxV));
        }

        public bool FinderTarget => AggroFinderAspect.Target != null;
        public bool TargetDistanceEary => math.distancesq(Transform.Position, AggroFinderAspect.TargetTransform.Position) <= MeshInfo.ValueRO.Bounds.max.z;

        public void Patrol(float dt)
        {
            if (math.distancesq(PatrolPosition.ValueRO.Position, Transform.Position) <= MeshInfo.ValueRO.Bounds.max.z)
                SetRandomPatrolPosition(Random.CreateFromIndex((uint)Entity.Index).NextUInt(uint.MaxValue));
            MoveTarget(PatrolPosition.ValueRO.Position, dt);
        }
        public void MoveTarget(float3 targetPosition, float dt, bool includeY = true)
        {
            if (includeY == false) targetPosition.y = Transform.Position.y;
            Transform.Position = math.lerp(Transform.Position, targetPosition, 0.1f * dt);
            Transform.LookAt(Transform.Position, targetPosition);
        }
    }
}
