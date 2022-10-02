using UnityEngine;

namespace Models
{
    public class TransformHelper : MonoBehaviour
    {
        public void SetScaleX(float x)
        {
            var scale = transform.localScale;
            scale.x = x;

            transform.localScale = scale;
        }

        public void SetScaleY(float y)
        {
            var scale = transform.localScale;
            scale.y = y;

            transform.localScale = scale;
        }
    }
}