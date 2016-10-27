using UnityEngine;
using UnityEngine.EventSystems;

namespace ConstantineSpace.Tools
{
    /// <summary>
    ///     Touch system interface.
    /// </summary>
    public interface ITouchHandler : IEventSystemHandler
    {
        void OnTouched(float touchTime);
    }

    /// <summary>
    ///     Checks touching and creates events.
    /// </summary>
    public class TouchManager : MonoBehaviour
    {
        private float _startTime;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startTime = Time.time;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                var touchTime = Time.time - _startTime;
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(position, Vector2.zero);
                if (hit.collider == null) return;

                ExecuteEvents.Execute<ITouchHandler>(hit.collider.gameObject, null,
                    (handler, data) => handler.OnTouched(touchTime));
            }
        }
    }
}