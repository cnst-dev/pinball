using System.Collections.Generic;
using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class ScreenManager : MonoBehaviour
    {
        [Header("MenuScreens")]
        [SerializeField]
        private GameObject _homeScreen;
        [SerializeField]
        private GameObject _gameScreen;


        private Dictionary<string, GameObject> _menuScreens;

        private GuiManager _guiManager;

        private void Awake()
        {
            _menuScreens = new Dictionary<string, GameObject>
            {
                {_homeScreen.name, _homeScreen}
            };
            _guiManager = GetComponent<GuiManager>();
        }

        private void OnEnable()
        {
            GameManager.Instance.OnStartStateSet += SetScreenOn;
            GameManager.Instance.OnEndStateSet += SetScreenOff;
        }

        private void SetScreenOn()
        {
            if (!_menuScreens.ContainsKey(GameManager.Instance.CurrentState + "Screen")) return;
            _guiManager.ScreenGoIn(_menuScreens[GameManager.Instance.CurrentState + "Screen"], 0.3f, 0.0f);
            _gameScreen.SetActive(false);
        }

        private void SetScreenOff()
        {
            if (!_menuScreens.ContainsKey(GameManager.Instance.CurrentState + "Screen")) return;
            _guiManager.ScreenGoOut(_menuScreens[GameManager.Instance.CurrentState + "Screen"], 0.3f, 0.0f);
            _gameScreen.SetActive(true);
        }

        private void OnDisable()
        {
            GameManager.Instance.OnStartStateSet -= SetScreenOn;
            GameManager.Instance.OnEndStateSet -= SetScreenOff;
        }

    }
}