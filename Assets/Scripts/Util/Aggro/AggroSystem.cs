using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Util.Aggro
{
    public partial struct AggroSystem : ISystem
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
            foreach (var finderAspect in SystemAPI.Query<AggroFinderAspect>())
            {
                var finderPosition = finderAspect.TransformSyncAspect.Position;
                float targetDistance = float.MaxValue;
                
                foreach (var targetAspect in SystemAPI.Query<AggroTargetAspect>())
                {
                   var dis = math.distancesq(finderPosition, targetAspect.TransformSyncAspect.Position);
                   if (dis < targetDistance)
                   {
                       targetDistance = dis;
                       finderAspect.Target = targetAspect.Entity;
                   }
                }
            }
        }
    }
}
