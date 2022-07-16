using System.Collections;
using Models;
using Models.Units;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controllers.Inputs
{
    public class InputUIController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Coroutine _inputCoroutine;

        [SerializeField]
        private RectTransform ownRectTransform;
        [SerializeField]
        private RectTransform blobRectTransform;
        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private Slider sliderX;
        [SerializeField]
        private Slider sliderY;

        [SerializeField]
        private float ratioX;
        [SerializeField]
        private float ratioY;
        [SerializeField]
        private float ratioSpeed;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!IsInRectangle(eventData, out var localPoint)) return;

            blobRectTransform.gameObject.SetActive(true);
            blobRectTransform.localPosition = localPoint;

            if (_inputCoroutine != null) StopCoroutine(_inputCoroutine);
            _inputCoroutine = StartCoroutine(HandleInput(eventData));
        }

        private bool IsInRectangle(in PointerEventData eventData, out Vector2 localPoint)
        {
            return RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                ownRectTransform,
                eventData.position,
                mainCamera,
                out localPoint
            );
        }

        private IEnumerator HandleInput(PointerEventData eventData)
        {
            while (isActiveAndEnabled)
            {
                IsInRectangle(eventData, out var localPoint);

                var localPosition = blobRectTransform.localPosition;
                sliderX.value = localPoint.x - localPosition.x;
                sliderY.value = localPoint.y - localPosition.y;

                var speed = ratioSpeed * Mathf.Max
                (
                    Mathf.Abs(sliderX.normalizedValue - 0.5f),
                    Mathf.Abs(sliderY.normalizedValue - 0.5f)
                );

                InputEvents.Move(speed * new Vector2(ratioX * sliderX.value, ratioY * sliderY.value).normalized);

                yield return null;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_inputCoroutine != null) StopCoroutine(_inputCoroutine);
            blobRectTransform.gameObject.SetActive(false);
        }
    }
}
