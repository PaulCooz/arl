using Common.Keys;
using Common.Storages.Preferences;
using UnityEngine;
using UnityEngine.Events;

namespace Models.Progressions
{
    public class LevelPointsHandler : MonoBehaviour
    {
        private int Points
        {
            get => Preference.Files.GetInt(StorageKey.Points, 0);
            set => Preference.Files.SetInt(StorageKey.Points, value);
        }
        private int AimPoints
        {
            get => Preference.Files.GetInt(StorageKey.AimPoints, 5);
            set => Preference.Files.SetInt(StorageKey.AimPoints, value);
        }

        [SerializeField]
        private UnityEvent onLevelUp;
        [SerializeField]
        private UnityEvent<float> onPointChange;

        public void AddPoints(int count)
        {
            Points += count;
            if (Points >= AimPoints)
            {
                Points -= AimPoints;
                AimPoints++;

                onLevelUp.Invoke();
            }

            onPointChange.Invoke((float) Points / AimPoints);
        }
    }
}