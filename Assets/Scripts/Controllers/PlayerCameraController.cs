using UnityEngine;

namespace Controllers
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;
        [SerializeField]
        private float speed;

        private void LateUpdate()
        {
            var nextPosition = Vector3.Lerp(transform.position, playerTransform.position, Time.deltaTime * speed);
            nextPosition.z = transform.position.z;

            transform.position = nextPosition;
        }
    }
}
