using System;
using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField]
        private int _bonus;

        private GameData _gameData;

        private void Start()
        {
            _gameData = FindObjectOfType<GameData>();
        }

        /// <summary>
        ///     Works when the ball contacts the obstacle.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name != "Ball") return;
            _gameData.UpdateScore(_bonus);
        }

        /// <summary>
        ///     Works when the ball contacts the footer border.
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name != "Ball") return;
            _gameData.StateMachine.SetState(GameState.End);
        }
    }
}