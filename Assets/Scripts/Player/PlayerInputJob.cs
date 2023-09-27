using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Player
{
    public partial struct PlayerInputJob : IJobEntity
    {
        public float DeltaTime;
        public PlayerInputData InputData;
        public void Execute(PlayerAspect aspect)
        {
            Move(aspect);
        }

        void Move(PlayerAspect aspect)
        {
            float3 position = float3.zero;

            float3 right = aspect.TransformSyncAspect.Transform.ValueRO.Right();
            float3 forward = aspect.TransformSyncAspect.Transform.ValueRO.Forward();
            if (InputData.IsLeft)
                position -= right;
            if (InputData.IsRight)
                position += right;
            if (InputData.IsFront)
                position += forward;
            if (InputData.IsBack)
                position -= forward;

            aspect.TransformSyncAspect.Transform.ValueRW.Position += position * DeltaTime;
        }
    }
}
