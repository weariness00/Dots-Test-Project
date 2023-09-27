using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace TransformSync
{
    public partial struct TransformSyncSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        }

        public void OnDestroy(ref SystemState state)
        {

        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (OT, transformSyncAspect) in SystemAPI.Query<TransformSyncData, TransformSyncAspect>())
            {
                OT.Transform.position = transformSyncAspect.Position;
                var rotate = quaternion.EulerXYZ(math.radians(transformSyncAspect.Rotate));
                // OT.Transform.rotation = transformSyncAspect.Transform.ValueRO.Rotation;
                OT.Transform.rotation = rotate;
                transformSyncAspect.Transform.ValueRW.Rotation = rotate;
            }
        }
    }
}
