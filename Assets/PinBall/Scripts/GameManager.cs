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
        Game,
        End
    }

    public class GameData : ScriptableObject
    {
        public Observer<int> ScoreObserver = new Observer<int>(0);

        public StateMachine<GameState> StateMachine = new StateMachine<GameState>();

        public bool UseAi;

        /// <summary>
        ///     Updates score.
        /// </summary>
        /// <param name="score">Additional score.</param>
        public void UpdateScore(int score)
        {
            ScoreObserver.Value += score;
        }

        /// <summary>
        ///     Returns the current score.
        /// </summary>
        /// <returns>The current score.</returns>
        public int GetScore()
        {
            return ScoreObserver.Value;
        }
    }

    public class GameManager : MonoBehaviour
    {
        public HomeScreen HomeScreen;
        public GameScreen GameScreen;

        private GameData _gameData;

        private void Awake()
        {
            _gameData = ScriptableObject.CreateInstance<GameData>();
            _gameData.StateMachine.AddState(GameState.Start, () => Debug.Log("Start State ON"), () => Debug.Log("Start State OFF"));
            _gameData.StateMachine.AddState(GameState.Home, HomeScreen.StartScreen, HomeScreen.StopScreen);
            _gameData.StateMachine.AddState(GameState.Game, () => GameScreen.StartScreen(_gameData), () => GameScreen.StopScreen(_gameData));
            _gameData.StateMachine.AddState(GameState.End, EndLevel, null);

            _gameData.StateMachine.SetState(GameState.Start);
        }

        private void Start()
        {
            _gameData.StateMachine.SetState(GameState.Home);

            HomeScreen.StartButton += () => StartLevel(false);
            HomeScreen.AiButton += () => StartLevel(true);
        }

        /// <summary>
        ///     Start the level manually.
        /// </summary>
        /// <param name="useAi">True for AI mode.</param>
        private void StartLevel(bool useAi)
        {
            _gameData.UseAi = useAi;
            _gameData.StateMachine.SetState(GameState.Game);
            SetTouchSender(!useAi);
            _gameData.UpdateScore(0);
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
            ScoreManager.SaveScore(_gameData.GetScore());
            SceneManager.LoadScene("Main");
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