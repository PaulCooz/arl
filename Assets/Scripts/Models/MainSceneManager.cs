using Common.Keys;
using Common.Storages;
using Common.Storages.Preferences;
using UnityEngine;
using UnityEngine.SceneManagement;
using Views;

namespace Models
{
    public class MainSceneManager : MonoBehaviour
    {
        [SerializeField]
        private MainSceneView mainScene;

        public void Continue()
        {
            SceneManager.LoadScene(Scenes.Game);
        }

        public void NewGame()
        {
            Preference.Game.ToDefault();
            SceneManager.LoadScene(Scenes.Game);
        }

        public void Sound()
        {
            Preference.Setting.Sound = !Preference.Setting.Sound;
            mainScene.UpdateButtons();
        }

        public void ServiceSingIn()
        {
            mainScene.UpdateButtons();
        }

        public void ServiceAchievements()
        {
            mainScene.UpdateButtons();
        }

        public void ServiceLeaderboard()
        {
            mainScene.UpdateButtons();
        }
    }
}