using UnityEngine;

namespace Models.Progressions
{
    [CreateAssetMenu]
    public class CardsObject : ScriptableObject
    {
        [SerializeField]
        private Card[] cards;

        public void GetCards(out Card left, out Card right)
        {
            var i = Random.Range(0, cards.Length);
            var j = Random.Range(0, cards.Length);

            if (i == j) j = (j + 1) % cards.Length;

            left = cards[i];
            right = cards[j];
        }
    }
}