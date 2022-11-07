using UnityEngine;
using Views.Animations;

namespace Models
{
    public class GameSceneManager : MonoBehaviour
    {
        [SerializeField]
        private ImageTweenView transferTween;
        [SerializeField]
        private GameMaster gameMaster;

        public void LevelDone()
        {
            transferTween.Alpha(1f, 0.5f, gameMaster.ClearOldLevel);
        }
    }
}