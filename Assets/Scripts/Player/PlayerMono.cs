using Unity.Entities;
using UnityEngine;

namespace Player
{
    public class PlayerMono : MonoBehaviour
    {
        public GameObject Player;
    }

    public class PlayerBaker : Baker<PlayerMono>
    {
        public override void Bake(PlayerMono authoring)
        {
            var entity = GetEntity(authoring.gameObject, TransformUsageFlags.Dynamic);
            AddComponentObject(entity, new PlayerObject(){GameObject = authoring.Player});
            AddComponent<PlayerTag>(entity);
        }
    }
}
