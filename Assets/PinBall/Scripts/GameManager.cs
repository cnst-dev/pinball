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
        Home,
        Game
    }

    public class GameManager : Singleton<GameManager>
    {
        public bool UseAi;
        public Observer<int> ScoreObserver;

        public HomeScreen HomeScreen;
        public GameScreen GameScreen;

        public StateMachine<GameState> StateMachine;

        public override void OnCreated()
        {
            ScoreObserver = new Observer<int>(0);

            StateMachine = new StateMachine<GameState>();
            StateMachine.AddState(GameState.Start, () => Debug.Log("Start State ON"), () => Debug.Log("Start State OFF"));
            StateMachine.AddState(GameState.Home, HomeScreen.StartScreen, HomeScreen.StopScreen);
            StateMachine.AddState(GameState.Game, GameScreen.StartScreen, GameScreen.StopScreen);

            StateMachine.SetState(GameState.Start);
        }

        private void Start()
        {
            StateMachine.SetState(GameState.Home);

            HomeScreen.StartButton += () => StartLevel(false);
            HomeScreen.AiButton += () => StartLevel(true);
        }

        /// <summary>
        ///     Start the level manually.
        /// </summary>
        /// <param name="useAi">True for AI mode.</param>
        private void StartLevel(bool useAi)
        {
            UseAi = useAi;
            StateMachine.SetState(GameState.Game);
            SetTouchSender(!useAi);
            ScoreObserver.Value = 0;
            if (useAi)
            {
                StartCoroutine(RandomForceLaunch(0.5f));
            }
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
            yield return new WaitForSeconds(delay);
            var touchTime = Random.Range(1.0f, 5.0f);
            FindObjectOfType<Launcher>().LaunchBall(touchTime);
        }
    }
}