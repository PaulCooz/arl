using UnityEngine;

namespace Models.Abstracts.Units
{
    public abstract class BaseUnit : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed;

        public void Translate(Vector2 delta)
        {
            var translation = (Time.deltaTime * moveSpeed) * delta;

            transform.Translate(new Vector3(translation.x, translation.y, 0), Space.World);
        }
    }
}