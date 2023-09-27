using Unity.Entities;
using Unity.Entities.Serialization;
using UnityEngine;

namespace Spawner
{
    public struct SpawnerProperty : IComponentData
    {
        public Entity Entity;
        
        public int SpawnCount;
        public int NowSpawnCount;
        public float SpawnIntervalTime;
        public float SpawnTimer;
    }

    public struct SpawnEntityTest : IComponentData
    {
        public EntityPrefabReference ep;
    }
}
