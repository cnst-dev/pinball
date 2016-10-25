using System;
using System.Collections;
using ConstantineSpace.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace ConstantineSpace.PinBall
{

    // All available game states.
    public enum GameState
    {
        Start,
        Menu,
        InGame
    }

    public class GameManager : Singleton<GameManager>
    {
        public bool UseAI;

        public Observer<GameState> GameStatusObserver;
        public Observer<int> ScoreObserver;

        public void Awake()
        {
            GameStatusObserver = new Observer<GameState>(GameState.Start);
            ScoreObserver = new Observer<int>(0);
        }

        private void Start()
        {
            SetGameState(GameState.Menu);
        }

        /// <summary>
        ///     Start the level or gameplay.
        /// </summary>
        /// <param name="useAI">True for AI mode.</param>
        public void StartLevel(bool useAI)
        {
            UseAI = useAI;
            SetGameState(GameState.InGame);
            SetTouchSender(!useAI);
            ScoreObserver.Value = 0;
            if (useAI)
            {
                StartCoroutine(RandomForceLaunch(0.5f));
            }
        }

        /// <summary>
        ///     Sets the game state.
        /// </summary>
        /// <param name="state">The new state.</param>
        private void SetGameState(GameState state)
        {
            GameStatusObserver.Value = state;
        }

        /// <summary>
        ///     Sets sender state.
        /// </summary>
        /// <param name="state">The new state.</param>
        private void SetTouchSender(bool state)
        {
            GetComponent<TouchManager>().enabled = state;
        }

        /// <summary>
        ///     Makes level end.
        /// </summary>
        public void EndLevel()
        {
            ScoreManager.SaveScore(ScoreObserver.Value);
            SceneManager.LoadScene("Main");
        }

        /// <summary>
        ///     Updates score.
        /// </summary>
        /// <param name="score">Additional score.</param>
        public void UpdateScore(int score)
        {
            ScoreObserver.Value += score;
        }

        /// <summary>
        ///     Launches the ball with random force.
        /// </summary>
        /// <returns></returns>
        private IEnumerator RandomForceLaunch(float delay)
        {
            var touchTime = Random.Range(1.0f, 5.0f);
            yield return new WaitForSeconds(delay);
            FindObjectOfType<Launcher>().LaunchBall(touchTime);

        }
    }
}