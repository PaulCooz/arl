using UnityEngine;

namespace Common.Configs
{
    [CreateAssetMenu]
    public class CardsConfigObject : ScriptableObject
    {
        [SerializeField]
        public Card[] cards;
    }
}