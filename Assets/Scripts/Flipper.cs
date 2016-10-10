using UnityEngine;

namespace ConstantineSpace.PinBall
{
    public class Flipper : MonoBehaviour
    {
        private HingeJoint2D _hingeJoint2D;
        /// <summary>
        ///     Initialization.
        /// </summary>
        public void Start()
        {
            _hingeJoint2D = GetComponent<HingeJoint2D>();
        }

        /// <summary>
        ///     Rotates up the flipper.
        /// </summary>
        private void OnMouseDown()
        {
            _hingeJoint2D.useMotor = true;
        }

        /// <summary>
        ///     Checks position and rotates the flipper down.
        /// </summary>
        public void Update()
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
    }
}