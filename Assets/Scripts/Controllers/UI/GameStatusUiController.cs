using System.Collections.Generic;
using Controllers.Units.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.UI
{
    public class GameStatusUiController : MonoBehaviour
    {
        private const float Epsilon = 0.001f;
        private const float AnimSpeedRatio = 8.0f;

        private Coroutine _changeHealthCoroutine;

        [SerializeField]
        private PlayerUnitController playerUnit;

        [SerializeField]
        private Slider healthSlider;

        public void PlayerHealthChange()
        {
            if (_changeHealthCoroutine != null) StopCoroutine(_changeHealthCoroutine);

            _changeHealthCoroutine = StartCoroutine(SmoothChangeHealth());
        }

        private IEnumerator<WaitForSeconds> SmoothChangeHealth()
        {
            var nextValue = (float) playerUnit.Health / playerUnit.MaxHealth;

            while (Mathf.Abs(nextValue - healthSlider.value) > Epsilon)
            {
                healthSlider.value += (nextValue - healthSlider.value) * AnimSpeedRatio * Time.deltaTime;

                yield return null;
            }

            healthSlider.value = nextValue;
            _changeHealthCoroutine = null;
        }
    }
}