using System.Collections;
using Controllers.Units.Enemies.Slime;
using UnityEngine;
using Views.Units;

namespace Controllers.Enemies.Slime
{
    public class SlimeMoveController : MonoBehaviour
    {
        [SerializeField]
        private SlimeUnitController slimeUnit;
        [SerializeField]
        private SlimeView slimeView;

        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float moveRadius;

        private void FixedUpdate()
        {
            var slimePosition = slimeUnit.OwnRigidbody.position;
            var playerPosition = slimeUnit.PlayerRigidbody.position;
            var direction = playerPosition - slimePosition;

            if (direction.sqrMagnitude < moveRadius)
            {
                slimeUnit.OwnRigidbody.velocity = direction.normalized * moveSpeed;
                slimeView.SetDirection(direction.x < 0f);
            }
        }
    }
}
