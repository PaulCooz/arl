using UnityEngine;

namespace Common.Keys
{
    public static class AnimationKey
    {
        public static readonly int RunProp = Animator.StringToHash("run");
        public static readonly int ShootTrigger = Animator.StringToHash("shoot");
        public static readonly int DieTrigger = Animator.StringToHash("die");
    }
}