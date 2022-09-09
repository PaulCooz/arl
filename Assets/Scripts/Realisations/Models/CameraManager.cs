using UnityEngine;

namespace Realisations.Models
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D player;

        private void Update()
        {
            if (player == null) return;

            var playerPos = player.transform.position;
            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
        }
    }
}