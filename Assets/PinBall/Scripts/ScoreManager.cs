using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class ScoreManager : MonoBehaviour
    {
        private static string _bestScoreTag = "BEST_SCORE";
        private static string _lastScoreTag = "LAST_SCORE";

        /// <summary>
        ///     Saves the best score.
        /// </summary>
        /// <param name="lastScore">The last score.</param>
        public static void SaveScore(int lastScore)
        {
            PlayerPrefs.SetInt(_lastScoreTag, lastScore);
            var bestScore = GetBestScore();
            if (lastScore > bestScore)
            {
                PlayerPrefs.SetInt(_bestScoreTag, lastScore);
            }
        }

        /// <summary>
        ///     Returns the best score.
        /// </summary>
        /// <returns>The best score.</returns>
        public static int GetBestScore()
        {
            return PlayerPrefs.GetInt(_bestScoreTag); ;
        }

        /// <summary>
        ///     Returns the last score.
        /// </summary>
        /// <returns>The last score.</returns>
        public static int GetLastScore()
        {
            return PlayerPrefs.GetInt(_lastScoreTag); ;
        }
    }
}