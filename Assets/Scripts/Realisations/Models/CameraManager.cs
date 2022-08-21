using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess

namespace Realisations.Models
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D player;

        private void Update()
        {
            var playerPos = player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        }
    }
}