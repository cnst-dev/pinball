using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class Flipper : MonoBehaviour
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
        ///     Rotates up the flipper. Runs when a message is received.
        /// </summary>
        private void OnTouch()
        {
            _hingeJoint2D.useMotor = true;
        }

        /// <summary>
        ///     Checks position and rotates the flipper down.
        /// </summary>
        private void Update()
        {
            if (_hingeJoint2D.limits.max > 0 && _hingeJoint2D.limitState == JointLimitState2D.UpperLimit)
            {
                _hingeJoint2D.useMotor = false;
            }
            else if (_hingeJoint2D.limits.max < 0 && _hingeJoint2D.limitState == JointLimitState2D.LowerLimit)
            {
                _hingeJoint2D.useMotor = false;
            }
        }

        /// <summary>
        ///     Uses to rotate the flipper in AI mode.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!GameManager.Instance.UseAI) return;
            _hingeJoint2D.useMotor = true;
        }
    }
}