using Common.Storages;
using Common.Storages.Preferences;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class MainSceneView : MonoBehaviour
    {
        [SerializeField]
        private GameObject continueGame;

        [SerializeField]
        private GameObject singIn;
        [SerializeField]
        private GameObject achievements;
        [SerializeField]
        private GameObject leaderboard;

        [SerializeField]
        private Image sound;
        [SerializeField]
        private Sprite[] soundStatus;

        private void Awake()
        {
            if (!Storage.HasPrefsFile) continueGame.SetActive(false);

            UpdateButtons();
        }

        public void UpdateButtons()
        {
            var isSoundOn = Preference.Sound;
            sound.sprite = soundStatus[isSoundOn ? 1 : 0];

            var isSingIn = Preference.ServiceSingIn;
            singIn.SetActive(!isSingIn);
            achievements.SetActive(isSingIn);
            leaderboard.SetActive(isSingIn);
        }
    }
}