using ConstantineSpace.PinBall;
using UnityEngine;

namespace ConstantineSpace.Tools
{
    /// <summary>
    ///     Use this class to make GameObject rotation.
    /// </summary>
    public class AutoRotate : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _rotationSpeed = new Vector3(0.0f, 0.0f, 5.0f);

        private void Update()
        {
            if (GameManager.Instance.GameStatusObserver.Value == GameState.InGame)
            {
                transform.Rotate(_rotationSpeed*Time.deltaTime);
            }
        }
    }
}