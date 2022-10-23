using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Controllers
{
    public class UiInputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private PointerEventData _pointer;
        private RectTransform _rectTransform;
        private Vector2? _startPosition;

        [SerializeField]
        private float maxDistance;

        [SerializeField]
        private InputEventsController inputEvents;
        [SerializeField]
        private UnityEvent<Vector2> onPointDown;
        [SerializeField]
        private UnityEvent<bool> changeEnable;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointer = eventData;
            _startPosition = eventData.position;
            changeEnable.Invoke(true);

            SetDownPosition();
        }

        private void SetDownPosition()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                _rectTransform,
                _pointer.position,
                _pointer.enterEventCamera,
                out var localPoint
            );

            onPointDown.Invoke(localPoint);
        }

        public void Update()
        {
            if (!_startPosition.HasValue) return;

            var distance = Vector2.Distance(_pointer.position, _startPosition.Value);
            var strength = Mathf.Clamp(distance, 0f, maxDistance) / maxDistance;
            var direction = (_pointer.position - _startPosition.Value).normalized;

            inputEvents.Move(strength * direction);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pointer = null;
            _startPosition = null;
            changeEnable.Invoke(false);
        }
    }
}