using System;
using Unity.Entities;
using Unity.Entities.Serialization;
using Unity.Scenes;
using UnityEngine;

namespace Spawner
{
    public class SpawnerMono : MonoBehaviour
    {
        public GameObject SpawnEntity;
        public int SpawnCount;
        public int SpawnIntervalTime;
    }

    public class SpawnBaker : Baker<SpawnerMono>
    {
        public override void Bake(SpawnerMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SpawnerProperty()
            {
                Entity = GetEntity(authoring.SpawnEntity, TransformUsageFlags.Dynamic),
                
                SpawnCount = authoring.SpawnCount,
                SpawnIntervalTime = authoring.SpawnIntervalTime,
                SpawnTimer = float.MaxValue,
            });
            
            AddComponent(entity, new SpawnEntityTest()
            {
                ep = new EntityPrefabReference(authoring.SpawnEntity),
            });

            AddComponent(entity, new PrefabLoadResult(){PrefabRoot = GetEntity(authoring.SpawnEntity, TransformUsageFlags.Dynamic) });
        }
    }
}