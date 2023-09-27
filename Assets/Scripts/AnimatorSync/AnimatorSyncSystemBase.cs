using Unity.Entities;

namespace AnimatorSync
{
    public partial class AnimatorSyncSystemBase : SystemBase
    {
        protected override void OnUpdate()
        {
            foreach (var (animator, aspect) in SystemAPI.Query<AnimatorSyncData, AnimatorSyncAspect>())
            {
                foreach (var parameter in aspect.TriggerHashes) animator.Animator.SetTrigger(parameter.Hash);
                foreach (var parameter in aspect.BoolHashes) animator.Animator.SetBool(parameter.Hash, parameter.Value);
                foreach (var parameter in aspect.FloatHashes) animator.Animator.SetFloat(parameter.Hash, parameter.Value);
                foreach (var parameter in aspect.IntHashes) animator.Animator.SetInteger(parameter.Hash, parameter.Value);
            }
        }
    }
}