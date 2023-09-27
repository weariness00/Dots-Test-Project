using Unity.Entities;
using UnityEngine;

namespace Util.Aggro
{
    public class AggroFinderMono : MonoBehaviour
    {
        public float findDistance;
    }

    public class AggroFinderBaker : Baker<AggroFinderMono>
    {
        public override void Bake(AggroFinderMono authoring)
        {
            var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
            AddComponent<AggroFinderTag>(entity);
            
            AddComponent(entity, new AggroFinderData()
            {
                TargetEntity = null,
                FindDistance = authoring.findDistance,
            }); 
        }
    }
}
