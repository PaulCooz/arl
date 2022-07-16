using System;
using UnityEngine;

namespace Controllers
{
    public enum GameState
    {
        Run,
        Pause
    }

    public enum UserInterfaceState
    {
        GameRun,
        GameWin,
        GameLose,
        MainMenu,
        MainSettings
    }

    public class StateController : MonoBehaviour
    {
        private GameState _currentGameState;
        private UserInterfaceState _currentUI;

        public GameState CurrentGameState
        {
            get => _currentGameState;
            private set
            {
                _currentGameState = value;
                OnChangeGameState?.Invoke();
            }
        }

        public UserInterfaceState CurrentUI
        {
            get => _currentUI;
            private set
            {
                _currentUI = value;
                OnChangeUI?.Invoke();
            }
        }

        public event Action OnChangeGameState;
        public event Action OnChangeUI;

        public void Initialization()
        {
            CurrentGameState = GameState.Pause;
            CurrentUI = UserInterfaceState.MainMenu;
        }
    }
}