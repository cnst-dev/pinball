using UnityEngine;

namespace ConstantineSpace.Tools
{
    /// <summary>
    /// Singleton pattern
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T: Component
    {
        private static T _instance;
        
        /// <summary>
        /// Singelton realization
        /// </summary>
        protected static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Instance initialization. Use base.Awake() when overriding
        /// </summary>
        public virtual void Awake()
        {
            _instance = this as T;
        }
    }
}
