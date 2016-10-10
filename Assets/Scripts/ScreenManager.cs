using ConstantineSpace.Tools;
using UnityEngine;

#pragma warning disable 649

namespace ConstantineSpace.PinBall
{
    public class ScreenManager : Singleton<ScreenManager>
    {
        [Header("Screens")]
        [SerializeField]
        private GameObject _homeScreen;
        [SerializeField]
        private GameObject _gameScreen;
        [SerializeField]
        private GameObject _pauseScreen;

        // The current screen game object.
        private GameObject _currentScreen;

        /// <summary>
        ///     Sets the Start screen.
        /// </summary>
        public void SetHomeScreen()
        {
            GuiManager.Instance.SetScreenState(_homeScreen, true);
            if (_currentScreen != null)
            {
                HideCurrentScreen();
                GuiManager.Instance.FadeBackground(true);
            }
            _currentScreen = _homeScreen;
        }

        /// <summary>
        ///     Sets the Game screen.
        /// </summary>
        public void SetGameScreen()
        {
            HideCurrentScreen();
            GuiManager.Instance.FadeBackground(false);
            GuiManager.Instance.SetScreenState(_gameScreen, true, 0);
            _currentScreen = _gameScreen;
        }

        /// <summary>
        ///     Hides the Game screen.
        /// </summary>
        public void HideGameScreen()
        {
            GuiManager.Instance.SetScreenState(_gameScreen, false);
        }

        /// <summary>
        ///     Sets the Pause screen.
        /// </summary>
        public void SetPauseScreen()
        {
            if (GameManager.Instance.CurrentState != GameManager.GameState.InGame)
            {
                return;
            }
            GuiManager.Instance.SetScreenState(_pauseScreen, true);
            GuiManager.Instance.FadeBackground(true);
            _currentScreen = _pauseScreen;
        }

        /// <summary>
        ///     Hide the current screen.
        /// </summary>
        public void HideCurrentScreen()
        {
            GuiManager.Instance.SetScreenState(_currentScreen, false);
            GuiManager.Instance.FadeBackground(false);
            _currentScreen = _gameScreen;
        }
    }
}