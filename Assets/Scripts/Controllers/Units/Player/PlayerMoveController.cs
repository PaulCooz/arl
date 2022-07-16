using Models;
using Models.Units;
using UnityEngine;

namespace Controllers.Units.Player
{
    public class PlayerMoveController : MonoBehaviour
    {
        private Vector2 _moveDelta;

        [SerializeField]
        private Rigidbody2D ownRigidbody2D;

        [SerializeField]
        private float speed;

        private void Start()
        {
            _moveDelta = Vector2.zero;

            InputEvents.OnMove += Move;
        }

        private void Move(Vector2 direction)
        {
            _moveDelta += Time.deltaTime * speed * direction;
        }

        private void FixedUpdate()
        {
            if (_moveDelta == Vector2.zero) return;

            ownRigidbody2D.velocity += _moveDelta;
            _moveDelta = Vector2.zero;
        }
    }
}
