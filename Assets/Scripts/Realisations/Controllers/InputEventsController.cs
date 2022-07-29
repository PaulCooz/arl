using UnityEngine;
using UnityEngine.Events;

namespace Realisations.Controllers
{
    public class InputEventsController : MonoBehaviour
    {
        [SerializeField]
        public UnityEvent<Vector2> onMove;

        public void Move(Vector2 n)
        {
            onMove?.Invoke(n);
        }
    }
}