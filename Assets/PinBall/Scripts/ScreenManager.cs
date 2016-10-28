using System.Collections.Generic;
using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class ScreenManager : MonoBehaviour
    {
        [Header("Screens")]
        [SerializeField]
        private GameObject _gameScreen;
        public GameObject[] MenuScreens;

        private Dictionary<string, GameObject> _menuScreens = new Dictionary<string, GameObject>();

        private GuiManager _guiManager;

        private void Awake()
        {
            _guiManager = GetComponent<GuiManager>();

            foreach (var screen in MenuScreens)
            {
                _menuScreens.Add(screen.name, screen);
            }
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