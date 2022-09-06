using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Realisations.Controllers
{
    public class UiInputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private PointerEventData _pointer;
        private RectTransform _rectTransform;
        private Vector2? _startPosition;

        [SerializeField]
        private InputEventsController inputEvents;
        [SerializeField]
        private UnityEvent<Vector2> onPointChange;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _pointer = eventData;
            _startPosition = eventData.position;
        }

        public void Update()
        {
            if (!_startPosition.HasValue) return;

            var rect = _rectTransform.rect;
            RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                _rectTransform,
                _pointer.position,
                _pointer.enterEventCamera,
                out var localPoint
            );
            onPointChange.Invoke(new Vector2(-localPoint.x / rect.height, -localPoint.y / rect.width));

            inputEvents.Move((_pointer.position - _startPosition.Value).normalized);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _pointer = null;
            _startPosition = null;
        }
    }
}