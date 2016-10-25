using System;
using ConstantineSpace.Tools;
using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class ScreenManager : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField]
        private GameObject _homeScreen;
        [SerializeField]
        private GameObject _gameScreen;

        // The current screen game object.
        private GameObject _currentScreen;

        private GuiManager _guiManager;

        private void Awake()
        {
            _guiManager = GetComponent<GuiManager>();
        }

        private void OnEnable()
        {
            GameManager.Instance.GameStatusObserver.OnValueChanged += SetScreen;
        }

        private void OnDisable()
        {
            GameManager.Instance.GameStatusObserver.OnValueChanged -= SetScreen;
        }

        /// <summary>
        ///     Sets the screen.
        /// </summary>
        private void SetScreen(object sender, ChangedValueArgs<GameState> args)
        {
            switch (args.Value)
            {
                case GameState.Menu:
                    SetHomeScreen();
                    break;
                case GameState.InGame:
                    SetGameScreen();
                    break;
                default:
                    HideGameScreen();
                    break;
            }
        }

        /// <summary>
        ///     Sets the Start screen.
        /// </summary>
        private void SetHomeScreen()
        {
            _guiManager.SetScreenState(_homeScreen, true);
            if (_currentScreen != null)
            {
                HideCurrentScreen();
                _guiManager.FadeBackground(true);
            }
            _currentScreen = _homeScreen;
        }

        /// <summary>
        ///     Sets the Game screen.
        /// </summary>
        private void SetGameScreen()
        {
            HideCurrentScreen();
            _guiManager.FadeBackground(false);
            _guiManager.SetScreenState(_gameScreen, true, 0);
            _currentScreen = _gameScreen;
        }

        /// <summary>
        ///     Hides the Game screen.
        /// </summary>
        private void HideGameScreen()
        {
            _guiManager.SetScreenState(_gameScreen, false);
        }

        /// <summary>
        ///     Hide the current screen.
        /// </summary>
        private void HideCurrentScreen()
        {
            _guiManager.SetScreenState(_currentScreen, false);
            _guiManager.FadeBackground(false);
            _currentScreen = _gameScreen;
        }
    }
}