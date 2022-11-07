using Common.Keys;
using Common.Storages.Preferences;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Views.Animations;

namespace Models
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField]
        private ImageTweenView transferTween;
        [SerializeField]
        private GameMaster gameMaster;

        [SerializeField]
        private UnityEvent onRestart;

        public void LevelDone()
        {
            transferTween.Alpha(1f, 0.5f, gameMaster.ClearOldLevel);
        }

        public void Restart()
        {
            transferTween.Alpha(1f, 0.5f, ToDefaultAndChangeLevel);
        }

        private void ToDefaultAndChangeLevel()
        {
            Preference.Game.ToDefault();

            onRestart.Invoke();
            gameMaster.DoneLevel();
        }

        public void ToMenuScene()
        {
            SceneManager.LoadScene(Scenes.Menu);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}