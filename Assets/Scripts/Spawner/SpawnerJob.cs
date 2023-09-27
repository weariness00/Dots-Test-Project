using Unity.Entities;
using UnityEngine;

namespace Spawner
{
    public partial struct SpawnerJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer.ParallelWriter ECB;

        private void Execute(SpawnerAspect aspect, [ChunkIndexInQuery]int sortKey)
        {
            aspect.Property.ValueRW.SpawnTimer += DeltaTime;
            if (aspect.CheckSpawn() == false) return;
            aspect.Property.ValueRW.SpawnTimer = 0f;
            aspect.Property.ValueRW.NowSpawnCount++;
            
            var newEntity = ECB.Instantiate(sortKey,aspect.Property.ValueRO.Entity);
            ECB.SetComponent(sortKey, newEntity, aspect.Transform.ValueRO);
        }
    }
}
