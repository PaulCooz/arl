using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Common.Animations
{
    public static class TweenExtensions
    {
        public static Coroutine ChangeValue
        (
            this MonoBehaviour monoBehaviour,
            Func<float> getter,
            Action<float> setter,
            float endValue,
            float duration,
            UnityAction onDone = null,
            AnimationCurve curve = null
        )
        {
            if (curve == null) curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

            return monoBehaviour.StartCoroutine(ChangeValueCoroutine(getter, setter, endValue, duration, onDone, curve));
        }

        private static IEnumerator<WaitForSeconds> ChangeValueCoroutine
        (
            Func<float> getter,
            Action<float> setter,
            float endValue,
            float duration,
            UnityAction onDone,
            AnimationCurve curve
        )
        {
            var time = 0f;
            var startVal = getter.Invoke();

            while (time <= duration)
            {
                yield return null;

                setter.Invoke(Mathf.Lerp(startVal, endValue, curve.Evaluate(time / duration)));
                time += Time.deltaTime;
            }

            setter.Invoke(endValue);
            onDone?.Invoke();
        }
    }
}