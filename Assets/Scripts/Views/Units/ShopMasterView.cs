using SettingObjects.Keys;
using UnityEngine;

namespace Views.Units
{
    public class ShopMasterView : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        public void StartGreetings()
        {
            animator.Play(AnimatorKeys.ShopMasterGreetings);
        }
    }
}
