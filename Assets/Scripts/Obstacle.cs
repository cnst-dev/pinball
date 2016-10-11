using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField]
        private int _bonus;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag != Ball.Tag) return;
            GameManager.Instance.UpdateScore(_bonus);
        }
    }
}