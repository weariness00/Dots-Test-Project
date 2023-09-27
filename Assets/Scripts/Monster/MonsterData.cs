using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Monster
{
    public class MonsterObject : IComponentData
    {
        public GameObject GameObject;
    }

    public struct MeshInfo : IComponentData
    {
        public Bounds Bounds;
    }

    public struct TargetEntity : IComponentData
    {
        public Entity Entity;
    }
}
