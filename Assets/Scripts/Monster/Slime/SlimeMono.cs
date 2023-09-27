using AnimatorSync;
using TransformSync;
using Unity.Entities;
using UnityEngine;

namespace Monster.Slime
{
    public class SlimeMono : MonsterMono
    {
    
    }
    
    public class SlimeBaker : Baker<SlimeMono>
    {
        public override void Bake(SlimeMono authoring)
        {
            var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
            AddComponent<SlimeTag>(entity);
            AddComponent<MonsterTag>(entity);
            AddComponent<AnimatorSyncPropertyInitTag>(entity);
            AddComponent<TransformSyncTag>(entity);
            
            AddComponent(entity, new MeshInfo(){Bounds = authoring.MeshRenderer.sharedMesh.bounds});

            AddComponentObject(entity, new MonsterObject(){GameObject = authoring.Monster});
        }
    }
}
