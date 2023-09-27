using Unity.Entities;
using Unity.Transforms;

namespace Spawner
{
    public readonly partial struct SpawnerAspect : IAspect
    {
        public readonly Entity Entity;

        public readonly RefRW<LocalTransform> Transform;
        public readonly RefRW<SpawnerProperty> Property;

        public bool CheckSpawn()    // true : spawn, false : unSpawn
        {
            return (Property.ValueRO.SpawnCount > Property.ValueRW.NowSpawnCount && 
                    Property.ValueRO.SpawnTimer >= Property.ValueRO.SpawnIntervalTime);
        }
        // private readonly RefRW<LocalTransform> _transform;
        // private readonly RefRW<SpawnProperty> _spawnProperty;
        //
        // public LocalTransform Transform => _transform.ValueRW;
        // public Entity SpawnPrefab => _spawnProperty.ValueRW.SpawnEntity;
        //
        // public int SpawnCount
        // {
        //     get => _spawnProperty.ValueRW.SpawnCount;
        //     set => _spawnProperty.ValueRW.SpawnCount = value;
        // }
    }
}
