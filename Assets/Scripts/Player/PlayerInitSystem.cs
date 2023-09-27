using Unity.Entities;
using UnityEngine;
using AnimatorSync;
using TransformSync;

namespace Player
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct PlayerInitSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
        
        }

        public void OnDestroy(ref SystemState state)
        {

        }

        public void OnUpdate(ref SystemState state)
        {
            var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

            foreach (var (go, entity) in SystemAPI.Query<PlayerObject>().WithEntityAccess())
            {
                var newGo = GameObject.Instantiate(go.GameObject);
                ecb.AddComponent(entity, new TransformSyncData(){Transform = newGo.transform});
                ecb.AddComponent(entity, new AnimatorSyncData(){Animator = newGo.GetComponent<Animator>()});
                ecb.AddComponent(entity, new PlayerInputData());
                
                ecb.AddComponent<TransformSyncInitTag>(entity);
                ecb.AddComponent<AnimatorSyncPropertyInitTag>(entity);
                ecb.RemoveComponent<PlayerObject>(entity);

                if(state.Enabled) state.Enabled = false;
            }
        }
    }
}
