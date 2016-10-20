using UnityEngine;

namespace ConstantineSpace.Tools
{
    /// <summary>
    ///     Singleton pattern.
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        /// <summary>
        ///     Singelton realization.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<T>();
                if (_instance != null) return _instance;
                var obj = new GameObject();
                _instance = obj.AddComponent<T>();
                return _instance;
            }
        }

        /// <summary>
        ///     Instance initialization.
        /// </summary>
        private void Awake()
        {
            _instance = this as T;
            OnCreated();
        }

        private void OnDestroy()
        {
            _instance = null;
            OnDestroyed();
        }

        /// <summary>
        ///     Use this method for overriding instead Awake().
        /// </summary>
        protected void OnCreated()
        {

        }

        /// <summary>
        ///     Use this method for overriding instead OnDestroy().
        /// </summary>
        protected void OnDestroyed()
        {

        }

        /// <summary>
        ///     Checks instance status.
        /// </summary>
        /// <returns></returns>
        public static bool IsCreated()
        {
            return _instance != null;
        }
    }
}
