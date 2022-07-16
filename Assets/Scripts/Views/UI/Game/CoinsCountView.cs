using TMPro;
using UnityEngine;

namespace Views.UI.Game
{
    public class CoinsCountView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text tmpCoins;

        public void ChangeCount(int count)
        {
            tmpCoins.text = $"coins: {count.ToString()}";
        }
    }
}