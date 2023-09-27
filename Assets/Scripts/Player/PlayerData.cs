using Unity.Entities;
using UnityEngine;

namespace Player
{
    public class PlayerObject : IComponentData
    {
        public GameObject GameObject;
    }

    public struct PlayerInputData : IComponentData
    {
        public bool IsRight;
        public bool IsLeft;
        public bool IsFront;
        public bool IsBack;

        public bool IsAttack;
        public bool IsJump;
        public bool IsDash;
    }
}
