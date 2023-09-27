using Spawner;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Scenes;

public partial struct TestSystem : ISystem
{
    public void OnStartRunning(ref SystemState state)
    {
        // Add the RequestEntityPrefabLoaded component to the Entities that have an
        // EntityPrefabReference but not yet have the PrefabLoadResult
        // (the PrefabLoadResult is added when the prefab is loaded)
        // Note: it might take a few frames for the prefab to be loaded
        var query = SystemAPI.QueryBuilder()
            .WithAll<SpawnEntityTest>()
            .WithNone<PrefabLoadResult>().Build();
        state.EntityManager.AddComponent<RequestEntityPrefabLoaded>(query);
    }

    
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
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        // For the Entities that have a PrefabLoadResult component (Unity has loaded
        // the prefabs) get the loaded prefab from PrefabLoadResult and instantiate it
        foreach (var (prefab, entity) in
                 SystemAPI.Query<RefRO<PrefabLoadResult>>().WithEntityAccess())
        {
            var instance = ecb.Instantiate(prefab.ValueRO.PrefabRoot);

            // Remove both RequestEntityPrefabLoaded and PrefabLoadResult to prevent
            // the prefab being loaded and instantiated multiple times, respectively
            ecb.RemoveComponent<RequestEntityPrefabLoaded>(entity);
            ecb.RemoveComponent<PrefabLoadResult>(entity);
        }

        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
