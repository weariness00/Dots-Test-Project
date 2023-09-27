using Unity.Entities;
using UnityEngine;

namespace Util.Aggro
{
    public class AggroTargetMono : MonoBehaviour
    {
    
    }
    
    public class AggroTargetBaker : Baker<AggroTargetMono>
    {
        public override void Bake(AggroTargetMono authoring)
        {
            var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
            AddComponent<AggroTargetTag>(entity);
        }
    }
}
