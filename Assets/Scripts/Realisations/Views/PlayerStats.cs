using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Realisations.Views
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField]
        private Image gunReload;
        [SerializeField]
        private TMP_Text healthText;

        private void Awake()
        {
            gunReload.fillAmount = 0f;
        }

        public void UpdateGunReload(float status)
        {
            gunReload.fillAmount = status;
        }

        public void UpdateHealth(int current, int max)
        {
            healthText.text = $"{current.ToString()}/{max.ToString()}";
        }
    }
}