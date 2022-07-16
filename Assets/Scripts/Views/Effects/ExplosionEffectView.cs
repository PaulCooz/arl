using UnityEngine;

namespace Views.Effects
{
    public class ExplosionEffectView : MonoBehaviour
    {
        public void OnAnimDone() // invoke by animator
        {
            Destroy(gameObject);
        }
    }
}