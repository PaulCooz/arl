using TMPro;
using UnityEngine;

namespace Views
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text healthText;

        public void UpdateHealth(int current)
        {
            healthText.text = current.ToString();
        }
    }
}