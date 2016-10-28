using System;
using ConstantineSpace.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace ConstantineSpace.PinBall
{
    public class HomeScreen : MonoBehaviour
    {
        [SerializeField]
        // The menu background image.
        private Image _menuBackground;

        [Header("Buttons")]
        [SerializeField]
        private Button _startButton;
        [SerializeField]
        private Button _aiButton;

        [Header("Texts")]
        [SerializeField]
        private Text _bestScoreText;
        [SerializeField]
        private Text _lastScoreText;

        // The menu background start color.
        private Color _menuBackgroundColor;

        public event Action StartButton;
        public event Action AiButton;

        private void Start()
        {
            _menuBackgroundColor = _menuBackground.color;
        }

        /// <summary>
        ///     Starts the screen.
        /// </summary>
        public void StartScreen()
        {
            _startButton.onClick.AddListener(() =>
            {
                if (StartButton != null) StartButton();
            });

            _aiButton.onClick.AddListener(() =>
            {
                if (AiButton != null) AiButton();
            });

            SetScoreTexts();
            ScreenGoIn(0.3f, 0.0f);
        }

        /// <summary>
        ///     Stops the screen.
        /// </summary>
        public void StopScreen()
        {
            _startButton.onClick.RemoveAllListeners();
            _aiButton.onClick.RemoveAllListeners();
            FadeBackground(false);
            ScreenGoOut(0.3f, 0.0f);
        }

        /// <summary>
        ///     The screen scale animation from 1 to 0.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="delay">The delay before the animation.</param>
        private void ScreenGoIn(float duration, float delay)
        {
            gameObject.SetActive(true);
            gameObject.transform.localScale = Vector3.zero;
            StartCoroutine(SimpleAnimator.ScaleAnimation(gameObject, 1, duration, delay, () =>
            {
                gameObject.transform.localScale = Vector3.one;
            }));
        }

        /// <summary>
        ///     The screen scale animation from 0 to 1.
        /// </summary>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="delay">The delay before the animation.</param>
        private void ScreenGoOut(float duration, float delay)
        {
            gameObject.transform.localScale = Vector3.one;
            StartCoroutine(SimpleAnimator.ScaleAnimation(gameObject, 0, duration, delay, () =>
            {
                gameObject.transform.localScale = Vector3.zero;
                gameObject.SetActive(false);
            }));
        }

        /// <summary>
        ///     Sets the score texts in menu.
        /// </summary>
        private void SetScoreTexts()
        {
            _bestScoreText.text = string.Format("Best: {0}", ScoreManager.GetBestScore());
            _lastScoreText.text = string.Format("Last: {0}", ScoreManager.GetLastScore());
        }

        /// <summary>
        ///     The background fade animation depending on the state.
        /// </summary>
        /// <param name="state">Fade in if true, fade out if false.</param>
        /// <param name="duration">The duration of the animation.</param>
        private void FadeBackground(bool state, float duration = 0.5f)
        {
            if (_menuBackground == null) return;
            _menuBackground.gameObject.SetActive(true);

            if (state)
            {
                StartCoroutine(SimpleAnimator.FadeAnimation(_menuBackground, duration, _menuBackgroundColor));
            }
            else
            {
                var newColor = _menuBackgroundColor;
                newColor.a = 0f;
                StartCoroutine(SimpleAnimator.FadeAnimation(_menuBackground, duration, newColor));
            }
        }
    }
}