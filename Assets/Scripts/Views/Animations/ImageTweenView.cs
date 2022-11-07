using Common.Animations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Views.Animations
{
    [RequireComponent(typeof(Image))]
    public class ImageTweenView : MonoBehaviour
    {
        private Image _image;

        private Image Image
        {
            get
            {
                if (_image == null) _image = GetComponent<Image>();
                return _image;
            }
        }

        [SerializeField]
        private UnityEvent onAlpha0;
        [SerializeField]
        private UnityEvent onAlpha1;

        public void Alpha(float value, float duration, UnityAction onDone)
        {
            this.ChangeValue
            (
                () => Image.color.a,
                v =>
                {
                    var color = Image.color;
                    color.a = v;
                    Image.color = color;

                    if (v <= 0f) onAlpha0.Invoke();
                    if (v >= 1f) onAlpha1.Invoke();
                },
                value,
                duration,
                onDone
            );
        }

        public void Alpha0(float duration)
        {
            Alpha(0f, duration, null);
        }

        public void Alpha1(float duration)
        {
            Alpha(1f, duration, null);
        }
    }
}