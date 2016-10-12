using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField]
        private float _force = 10.0f;

        /// <summary>
        ///     Runs when a message is received.
        /// </summary>
        private void OnTouch(float touchTime)
        {
            touchTime = Mathf.Clamp(touchTime, 1.0f, 5.0f);
            LaunchBall(touchTime);
        }

        /// <summary>
        ///     Launches the ball.
        /// </summary>
        private void LaunchBall(float touchTime)
        {
            GameManager.Instance.Ball.AddForce(new Vector2(0.0f, touchTime*_force), ForceMode2D.Impulse);
            gameObject.SetActive(false);
        }
    }
}