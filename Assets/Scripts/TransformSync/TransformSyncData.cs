using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace TransformSync
{
    public class TransformSyncData : IComponentData
    {
        public Transform Transform;
    }
    
    public struct TransformSyncTag : IComponentData{}
    public struct TransformSyncInitTag : IComponentData{}

    public struct TransformRotateData : IComponentData
    {
        public float3 Rotate;
    }
}
