using Unity.Entities;
using UnityEngine;

namespace AnimatorSync
{
    public class AnimatorSyncData : IComponentData
    {
        public Animator Animator;
    }

    public struct AnimatorSyncPropertyInitTag : IComponentData{}
    public struct AnimatorSyncTag : IComponentData{}
    
    public struct AnimatorTriggerHashes : IBufferElementData { public int Hash; }
    public struct AnimatorBoolHashes : IBufferElementData { public int Hash; public bool Value; }
    public struct AnimatorFloatHashes : IBufferElementData { public int Hash; public float Value; }
    public struct AnimatorIntHashes : IBufferElementData {public int Hash; public int Value; }
}