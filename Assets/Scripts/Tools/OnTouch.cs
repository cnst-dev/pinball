using UnityEngine;

namespace ConstantineSpace.Tools
{
    /// <summary>
    ///     Uses for sending messages for touched objects.
    /// </summary>
    public class OnTouch : MonoBehaviour
    {
        public void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(position, Vector2.zero);
            if (hit.collider != null && hit.transform.tag == "CanTouch")
            {
               hit.transform.gameObject.SendMessage("OnTouch");
            }
        }
    }
}