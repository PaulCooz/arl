using UnityEngine;

namespace Views
{
    public class SpriteEffect : MonoBehaviour
    {
        public void AnimationDone() // call in animator
        {
            Destroy(gameObject);
        }
    }
}