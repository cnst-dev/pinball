using ConstantineSpace.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace ConstantineSpace.PinBall
{
    public class GameScreen : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField]
        private Text _scoreText;

        /// <summary>
        ///     Starts the screen.
        /// </summary>
        public void StartScreen(GameData gameData)
        {
            gameData.ScoreObserver.OnValueChanged += SetScoreText;
            gameObject.SetActive(true);
        }

        /// <summary>
        ///     Stops the screen.
        /// </summary>
        public void StopScreen(GameData gameData)
        {
            gameData.ScoreObserver.OnValueChanged -= SetScoreText;
            gameObject.SetActive(false);
        }

        /// <summary>
        ///     Sets the score text.
        /// </summary>
        private void SetScoreText(object sender, ChangedValueArgs<int> args)
        {
            _scoreText.text = args.Value.ToString("0000");
        }
    }
}