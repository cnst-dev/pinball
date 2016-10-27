using ConstantineSpace.Tools;
using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class Launcher : MonoBehaviour, ITouchHandler
    {
        [Header("Config")]
        [SerializeField]
        private Rigidbody2D _ball;
        [SerializeField]
        private readonly float _force = 10.0f;

        /// <summary>
        ///     Launches the ball.
        /// </summary>
        public void LaunchBall(float touchTime)
        {
            touchTime = Mathf.Clamp(touchTime, 1.0f, 5.0f);
            _ball.AddForce(new Vector2(0.0f, touchTime*_force), ForceMode2D.Impulse);
            gameObject.SetActive(false);
        }

        /// <summary>
        ///     Touch event receiver.
        /// </summary>
        /// <param name="touchTime">The touch time.</param>
        public void OnTouched(float touchTime)
        {
            LaunchBall(touchTime);
        }
    }
}