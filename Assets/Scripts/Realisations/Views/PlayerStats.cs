﻿using TMPro;
using UnityEngine;

namespace Realisations.Views
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text healthText;

        public void UpdateHealth(int current, int max)
        {
            healthText.text = $"{current.ToString()}/{max.ToString()}";
        }
    }
}