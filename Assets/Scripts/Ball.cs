using UnityEngine;
using UnityEngine.SceneManagement;

namespace ConstantineSpace.PinBall
{
    public class Ball : MonoBehaviour
    {
        private void Update()
        {
            if (!(transform.position.y < -5.0f)) return;
            SceneManager.LoadScene("Main");
        }
    }
}