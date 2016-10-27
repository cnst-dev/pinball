using ConstantineSpace.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace ConstantineSpace.PinBall
{
    public class GuiManager : MonoBehaviour
    {
        [SerializeField]
        // The menu background image.
        private Image _menuBackground;

        [Header("Text objects")]
        [SerializeField]
        private Text _scoreText;
        [SerializeField]
        private Text _bestScoreText;
        [SerializeField]
        private Text _lastScoreText;

        // The menu background start color.
        private Color _menuBackgroundColor;

        private void OnEnable()
        {
            GameManager.Instance.ScoreObserver.OnValueChanged += SetScoreText;
            GameManager.Instance.OnStartStateSet += SetHomeScoreTexts;
        }

        private void OnDisable()
        {
            GameManager.Instance.ScoreObserver.OnValueChanged -= SetScoreText;
            GameManager.Instance.OnStartStateSet -= SetHomeScoreTexts;
        }

        private void Start()
        {
            // Save start background color
            _menuBackgroundColor = _menuBackground.color;
        }

        /// <summary>
        ///     The screen scale animation from 1 to 0.
        /// </summary>
        /// <param name="screen">The screen game object for the scaling animation.</param>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="delay">The delay before the animation.</param>
        public void ScreenGoOut(GameObject screen, float duration, float delay)
        {
            screen.transform.localScale = Vector3.one;
            FadeBackground(false);
            StartCoroutine(SimpleAnimator.ScaleAnimation(screen, 0, duration, delay, () =>
            {
                screen.transform.localScale = Vector3.zero;
                screen.SetActive(false);
            }));
        }

        /// <summary>
        ///     The screen scale animation from 0 to 1.
        /// </summary>
        /// <param name="screen">The screen game object for the scaling animation.</param>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="delay">The delay before the animation.</param>
        public void ScreenGoIn(GameObject screen, float duration, float delay)
        {
            screen.transform.localScale = Vector3.zero;
            StartCoroutine(SimpleAnimator.ScaleAnimation(screen, 1, duration, delay, () =>
            {
                screen.transform.localScale = Vector3.one;
                screen.SetActive(true);
            }));
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

        /// <summary>
        ///     Sets the score text in game.
        /// </summary>
        private void SetScoreText(object sender, ChangedValueArgs<int> args)
        {
            _scoreText.text = args.Value.ToString("0000");
        }

        /// <summary>
        ///     Sets the score texts in menu.
        /// </summary>
        private void SetHomeScoreTexts()
        {
            if (GameManager.Instance.CurrentState != GameState.Home) return;
            _bestScoreText.text = string.Format("Best: {0}", ScoreManager.GetBestScore());
            _lastScoreText.text = string.Format("Last: {0}", ScoreManager.GetLastScore());
        }
    }
}