using System.Collections;
using Models;
using SettingObjects.Keys;
using UnityEngine;

namespace Controllers.Units.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private Coroutine _lockCoroutine;
        private bool _isRunning;
        private bool _isAlive;

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private Transform playerTransform;

        [SerializeField]
        private float changeDirectionSpeed;

        private void Start()
        {
            _isRunning = false;
            _isAlive = true;

            InputEvents.OnMove += OnMove;
        }

        public void PlayerDie()
        {
            _isAlive = false;
            animator.Play(AnimatorKeys.Die);
        }

        private void OnMove(Vector2 direction)
        {
            if (!_isAlive) return;

            CheckDirection(direction);
            CheckRunAnim();
        }

        private void CheckRunAnim()
        {
            if (_isRunning) return;

            _isRunning = true;
            animator.Play(AnimatorKeys.PlayerRun);
        }

        private void CheckDirection(in Vector2 direction)
        {
            if (direction.x == 0) return;
            if (_lockCoroutine != null) StopCoroutine(_lockCoroutine);

            _lockCoroutine = StartCoroutine(Lock(direction.x > 0));
        }

        private IEnumerator Lock(bool isRight)
        {
            var lockDirection = isRight ? 1f : -1f;
            while (lockDirection * playerTransform.localScale.x < 1f)
            {
                var localScale = playerTransform.localScale;
                localScale.x = Mathf.Clamp(localScale.x + lockDirection * changeDirectionSpeed * Time.deltaTime, -1f, 1f);

                playerTransform.localScale = localScale;

                yield return null;
            }

            _lockCoroutine = null;
        }

        public void PlayShoot()
        {
            if (!_isAlive) return;

            animator.Play(AnimatorKeys.PlayerShoot);
        }

        public void RunAnimDone() // called on run end anim
        {
            _isRunning = false;
        }

        public void ShootAnimDone() // called on shoot end anim
        {
            if (_isRunning) RunAnimDone();
        }
    }
}
