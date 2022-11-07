using Common.Animations;
using UnityEngine;
using UnityEngine.Events;

namespace Views.Animations
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupTweenView : MonoBehaviour
    {
        private CanvasGroup _group;

        private CanvasGroup Group
        {
            get
            {
                if (_group == null) _group = GetComponent<CanvasGroup>();
                return _group;
            }
        }

        public void Alpha(float value, float duration, UnityAction onDone = null)
        {
            this.ChangeValue
            (
                () => Group.alpha,
                v => Group.alpha = v,
                value,
                duration,
                onDone
            );
        }

        public void Alpha0(float duration)
        {
            Alpha(0, duration);
        }

        public void Alpha1(float duration)
        {
            Alpha(1, duration);
        }
    }
}