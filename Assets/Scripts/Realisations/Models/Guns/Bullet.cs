using UnityEngine;

namespace Realisations.Models.Guns
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D bulletRigidbody;
        [SerializeField]
        private float force;

        public void Push(in Vector2 direction)
        {
            bulletRigidbody.velocity = direction * force;
        }
    }
}