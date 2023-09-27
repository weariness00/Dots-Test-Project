using AnimatorSync;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Player
{
    public partial struct PlayerInputSystem : ISystem
    {
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Speed = Animator.StringToHash("Speed");

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        public void OnUpdate(ref SystemState state)
        {
            float dt = SystemAPI.Time.DeltaTime;
            var input = new PlayerInputData()
            {
                IsLeft = Input.GetKey(KeyCode.A),
                IsRight = Input.GetKey(KeyCode.D),
                IsFront = Input.GetKey(KeyCode.W),
                IsBack = Input.GetKey(KeyCode.S),

                IsJump = Input.GetKey(KeyCode.Space),
                IsAttack = Input.GetMouseButtonDown(0),
                IsDash = Input.GetKey(KeyCode.LeftShift),
            };
            var inputJob = new PlayerInputJob() {DeltaTime = dt, InputData = input};
            state.Dependency = inputJob.ScheduleParallel(state.Dependency);

            foreach (var (PA, tag) in SystemAPI.Query<AnimatorSyncData, PlayerTag>())
            {
                if (input.IsAttack) PA.Animator.SetTrigger(Attack);
                PA.Animator.SetFloat(Speed, IsMoveInput(input) ? 1 : 0);
            }
            
            if (Input.GetMouseButtonUp(1))
            {
                Cursor.visible = !Cursor.visible;
                Cursor.lockState = Cursor.visible ? Cursor.lockState = CursorLockMode.None : CursorLockMode.Locked;
            }
        }

        bool IsMoveInput(PlayerInputData inputData)
        {
            if (inputData.IsFront || inputData.IsBack ||
                inputData.IsRight || inputData.IsLeft)
                return true;
            return false;
        }
    }
}
