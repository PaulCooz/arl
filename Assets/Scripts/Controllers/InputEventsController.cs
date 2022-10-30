using UnityEngine;
using UnityEngine.Events;

namespace Controllers
{
    public class InputEventsController : MonoBehaviour
    {
        [SerializeField]
        public UnityEvent<Vector2> onMove;
        [SerializeField]
        public UnityEvent useLeft;
        [SerializeField]
        public UnityEvent useRight;

        public void Move(Vector2 n)
        {
            if (!enabled) return;

            onMove?.Invoke(n);
        }
    }
}