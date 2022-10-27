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
            SceneManager.LoadScene("GameScene");
        }

        public void NewGame()
        {
            if (Storage.HasPrefsFile) Preference.Files.DeleteAll();

            SceneManager.LoadScene("GameScene");
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