using Unity.Entities;
using UnityEngine;
using AnimatorSync;
using TransformSync;
using Unity.VisualScripting;

namespace Monster
{
    public class MonsterMono : MonoBehaviour
    {
        public GameObject Monster;
        public SkinnedMeshRenderer MeshRenderer;
    }

    public class MonsterBaker : Baker<MonsterMono>
    {
        public override void Bake(MonsterMono authoring)
        {
            var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
            AddComponent<MonsterTag>(entity);
            AddComponent<AnimatorSyncPropertyInitTag>(entity);
            AddComponent<TransformSyncTag>(entity);
            
            AddComponent(entity, new MeshInfo(){Bounds = authoring.MeshRenderer.sharedMesh.bounds});

            AddComponentObject(entity, new MonsterObject(){GameObject = authoring.Monster});
        }
    }
}
