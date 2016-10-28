using System.Collections;
using ConstantineSpace.Tools;
using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class Flipper : MonoBehaviour, ITouchHandler
    {
        private HingeJoint2D _hingeJoint2D;

        /// <summary>
        ///     Initialization.
        /// </summary>
        private void Start()
        {
            _hingeJoint2D = GetComponent<HingeJoint2D>();
        }

        /// <summary>
        ///     Rotates up the flipper.
        /// </summary>
        private void RotateUp()
        {
            _hingeJoint2D.useMotor = true;
            StartCoroutine(RotateDown());
        }

        /// <summary>
        ///     Touch event receiver.
        /// </summary>
        /// <param name="touchTime">The touch time.</param>
        public void OnTouched(float touchTime)
        {
            RotateUp();
        }

        /// <summary>
        ///     Uses to rotate the flipper in AI mode.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!GameManager.Instance.UseAi) return;
            _hingeJoint2D.useMotor = true;
            StartCoroutine(RotateDown());
        }

        /// <summary>
        ///     Checks position and rotates the flipper down.
        /// </summary>
        /// <returns></returns>
        private IEnumerator RotateDown()
        {
            if (_hingeJoint2D.limits.max > 0)
            {
                yield return new WaitUntil(() => _hingeJoint2D.limitState == JointLimitState2D.UpperLimit);
            }
            else
            {
                yield return new WaitUntil(() => _hingeJoint2D.limitState == JointLimitState2D.LowerLimit);
            }
            _hingeJoint2D.useMotor = false;
        }
    }
}