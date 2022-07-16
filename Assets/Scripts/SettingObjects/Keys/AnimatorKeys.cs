using UnityEngine;

namespace SettingObjects.Keys
{
    public static class AnimatorKeys
    {
        public static readonly int SlimeAttack = Animator.StringToHash("Attack");
        public static readonly int PlayerRun = Animator.StringToHash("Run");
        public static readonly int PlayerShoot = Animator.StringToHash("Shoot");
        public static readonly int ShopMasterGreetings = Animator.StringToHash("Greetings");
        public static readonly int Die = Animator.StringToHash("Die");
    }
}
