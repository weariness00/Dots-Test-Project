using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace TransformSync
{
    public readonly partial struct TransformSyncAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly RefRW<LocalTransform> Transform;

        private readonly RefRO<TransformSyncTag> _tag;
        private readonly RefRW<TransformRotateData> _rotate;

        public float3 Position
        {
            get => Transform.ValueRO.Position;
            set => Transform.ValueRW.Position = value;
        }
        
        public float3 Rotate
        {
            get => _rotate.ValueRO.Rotate;
            set => _rotate.ValueRW.Rotate = value;
        }

        public quaternion LookAt(float3 myPosition, float3 targetPosition)
        {
            var lookRotate = quaternion.LookRotation(targetPosition - myPosition, new float3(0f, 1f, 0f));
            Transform.ValueRW.Rotation = lookRotate;
            Vector3 angle = ((Quaternion)lookRotate).eulerAngles;
            Rotate = angle;
            return lookRotate;
        }
    }
}
