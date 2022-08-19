using UnityEngine;

namespace Realisations.Models
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField]
        private Transform playerTransform;

        public void SyncWithPlayer()
        {
            var cam = transform;
            var player = playerTransform.transform.position;

            var pos = cam.position;
            pos.x = player.x;
            pos.y = player.y;

            cam.position = pos;
        }
    }
}