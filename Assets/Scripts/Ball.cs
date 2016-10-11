using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class Ball : MonoBehaviour
    {
        public static string Tag = "Ball";

        private void Update()
        {
            if (!(transform.position.y < -5.0f)) return;
            GameManager.Instance.EndLevel();
        }
    }
}