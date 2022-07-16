using System.Collections;
using SettingObjects.Keys;
using UnityEngine;

namespace Views.Units
{
    public class SlimeView : MonoBehaviour
    {
        private Coroutine _lockCoroutine;

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float speed;

        public void PlayAttack()
        {
            animator.Play(AnimatorKeys.SlimeAttack);
        }

        public void SetDirection(in bool isRight)
        {
            if (_lockCoroutine != null) StopCoroutine(_lockCoroutine);

            _lockCoroutine = StartCoroutine(Lock(isRight));
        }

        private IEnumerator Lock(bool isRight)
        {
            var lockDirection = isRight ? 1f : -1f;
            while (lockDirection * transform.localScale.x < 1f)
            {
                var localScale = transform.localScale;
                localScale.x = Mathf.Clamp(localScale.x + lockDirection * speed * Time.deltaTime, -1f, 1f);

                transform.localScale = localScale;

                yield return null;
            }

            _lockCoroutine = null;
        }
    }
}
