using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class Launcher : MonoBehaviour
    {
        /// <summary>
        ///     Runs when a message is received.
        /// </summary>
        private void OnTouch()
        {
            LaunchBall();
        }

        /// <summary>
        ///     Launches the ball.
        /// </summary>
        private void LaunchBall()
        {
            GameManager.Instance.Ball.AddForce(new Vector2(0.0f, 20.0f), ForceMode2D.Impulse);
            gameObject.SetActive(false);
        }
    }
}