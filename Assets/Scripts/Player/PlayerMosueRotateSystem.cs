using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Player
{
    public partial struct PlayerMouseRotateSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            float3 mousetAxis = new float3(Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse X"),0f );
            new PlayerMouseRotateJob()
            {
                DeltaTime = deltaTime,
                MouseAxis = mousetAxis,
            }.ScheduleParallel();
        }
    }
    
    public partial struct PlayerMouseRotateJob : IJobEntity
    {
        public float DeltaTime;
        public float3 MouseAxis;
        
        void Execute(PlayerAspect aspect, [ChunkIndexInQuery]int sortKey)
        {
            MouseAxis *= DeltaTime * 100;

            aspect.TransformSyncAspect.Rotate += MouseAxis;

            // if(MouseAxis.x == 0f) return;
            // float4 rotate = aspect.Transform.ValueRO.Rotation.value;
            // rotate.y += math.radians(MouseAxis.x);
            // if (rotate.y > 1.0f) rotate.y -= 2;
            // else if (rotate.y < -1.0f) rotate.y += 2;

            // aspect.Transform.ValueRW.Rotation =  new quaternion(rotate);
        }
    }
}
