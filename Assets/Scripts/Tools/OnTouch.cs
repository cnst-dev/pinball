using UnityEngine;

namespace ConstantineSpace.Tools
{
    /// <summary>
    ///     Sends messages for touched objects - flippers and launcher.
    /// </summary>
    public class OnTouch : MonoBehaviour
    {
        private float _startTime;

        public void Update()
        {
//            if (!Input.GetMouseButtonDown(0)) return;
            if (Input.GetMouseButtonDown(0))
            {
                _startTime = Time.time;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                var touchTime = Time.time - _startTime;
                var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(position, Vector2.zero);
                if (hit.collider != null && hit.transform.tag == "CanTouch")
                {
                    hit.transform.gameObject.SendMessage("OnTouch", touchTime);
                }
            }
        }
    }
}