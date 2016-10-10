using System.Reflection;
using ConstantineSpace.Tools;
using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class GameManager : Singleton<GameManager>
    {
        public Rigidbody2D Ball;

        // All available game states.
        public enum GameState
        {
            Menu,
            InGame,
            Paused
        }

        // The current state of the game.
        public GameState CurrentState { get; private set; }

        /// <summary>
        ///     Initialization.
        /// </summary>
        private void Start()
        {
            ScreenManager.Instance.SetHomeScreen();
            SetGameState(GameState.Menu);
        }

        /// <summary>
        ///     Start the level or gameplay.
        /// </summary>
        public void StartLevel()
        {
            ScreenManager.Instance.SetGameScreen();
            SetGameState(GameState.InGame);
            SetTouchSender(true);
        }

        /// <summary>
        ///     Pause the game.
        /// </summary>
        public void Pause()
        {
            ScreenManager.Instance.SetPauseScreen();
            SetGameState(GameState.Paused);
            SetTouchSender(false);
        }

        /// <summary>
        ///     Continue the game.
        /// </summary>
        public void UnPause()
        {
            ScreenManager.Instance.HideCurrentScreen();
            SetGameState(GameState.InGame);
            SetTouchSender(true);
        }

        /// <summary>
        ///     Go to the Home screen.
        /// </summary>
        public void GoToHome()
        {
            ScreenManager.Instance.HideGameScreen();
            ScreenManager.Instance.SetHomeScreen();
            SetGameState(GameState.Menu);

        }

        /// <summary>
        ///     Sets the game state.
        /// </summary>
        /// <param name="state">The new state.</param>
        private void SetGameState(GameState state)
        {
            CurrentState = state;
        }

        /// <summary>
        ///     Sets sender state.
        /// </summary>
        /// <param name="state">The new state.</param>
        private void SetTouchSender(bool state)
        {
            GetComponent<OnTouch>().enabled = state;
        }
    }
}