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
        private Card[] cards;

        [SerializeField]
        private UnityEvent onHide;
        [SerializeField]
        private UnityEvent onShow;
        [SerializeField]
        private UnityEvent<string> setLeftCard;
        [SerializeField]
        private UnityEvent<string> setRightCard;

        private void GetCards(out Card left, out Card right)
        {
            var i = Random.Range(0, cards.Length);
            var j = Random.Range(0, cards.Length);

            if (i == j) j = (j + 1) % cards.Length;

            left = cards[i];
            right = cards[j];
        }

        public void OnLevelUp()
        {
            onShow.Invoke();

            GetCards(out _left, out _right);

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