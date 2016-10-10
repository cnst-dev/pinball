using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ConstantineSpace.Tools
{
    /// <summary>
    /// Use this static methods for animations.
    /// </summary>
    public static class SimpleAnimator
    {
        /// <summary>
        /// Scale animation.
        /// </summary>
        /// <param name="target">The target game object for the scaling animation.</param>
        /// <param name="scale">Scale to.</param>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="delay">The delay before the animation.</param>
        /// <param name="callback">Callback.</param>
        public static IEnumerator ScaleAnimation(GameObject target, float scale, float duration, float delay, Action callback)
        {
            target.SetActive(true);

            yield return new WaitForSeconds(delay);

            var originalScale = target.transform.localScale;
            var targetScale = Vector3.one * scale;
            var originalTime = duration;

            while (duration > 0.0f)
            {
                duration -= Time.deltaTime;
                target.transform.localScale = Vector3.Lerp(targetScale, originalScale, duration / originalTime);
                yield return 0;
            }

            if (callback != null)
            {
                callback();
            }
        }

        /// <summary>
        /// Fade animation.
        /// </summary>
        /// <param name="target">The target game object for the fading animation.</param>
        /// <param name="duration">The duration of the animation.</param>
        /// <param name="color">The new color.</param>
        public static IEnumerator FadeAnimation(Image target, float duration, Color color)
        {
            if (target == null)
            {
                yield break;
            }

            var alpha = target.color.a;

            for (var t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
            {
                var newColor = new Color(color.r, color.g, color.b, Mathf.SmoothStep(alpha, color.a, t));
                target.color = newColor;
                yield return null;
            }
            target.color = color;

        }
    }
}