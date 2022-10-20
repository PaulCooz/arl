using Common.Interpreters;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Progressions
{
    public class CardsManager : MonoBehaviour
    {
        private Card _left;
        private Card _right;

        [SerializeField]
        private CardsObject cardsObject;

        [SerializeField]
        private UnityEvent onHide;
        [SerializeField]
        private UnityEvent onShow;
        [SerializeField]
        private UnityEvent<string> setLeftCard;
        [SerializeField]
        private UnityEvent<string> setRightCard;

        public void OnLevelUp()
        {
            onShow.Invoke();

            cardsObject.GetCards(out _left, out _right);

            setLeftCard.Invoke(_left.description);
            setRightCard.Invoke(_right.description);
        }

        public void Use(bool isLeft)
        {
            var script = new Script();
            script.Run(isLeft ? _left.command : _right.command);

            onHide.Invoke();
        }
    }
}