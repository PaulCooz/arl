using Common.Storages.Preferences;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Progressions
{
    public class LevelPointsHandler : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onLevelUp;
        [SerializeField]
        private UnityEvent<float> onPointChange;

        private void Awake()
        {
            UpdateProgress();
        }

        public void AddPoints(int count)
        {
            Preference.Points += count;
            if (Preference.Points >= Preference.AimPoints)
            {
                Preference.Points -= Preference.AimPoints;
                Preference.AimPoints++;

                onLevelUp.Invoke();
            }

            UpdateProgress();
        }

        private void UpdateProgress()
        {
            onPointChange.Invoke(1f - (float) Preference.Points / Preference.AimPoints);
        }
    }
}