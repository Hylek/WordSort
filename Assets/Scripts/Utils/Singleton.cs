using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;

                _instance = FindObjectOfType<T>();

                if (_instance != null) return _instance;

                var gameObject = new GameObject
                {
                    name = typeof(T).Name
                };
                _instance = gameObject.AddComponent<T>();

                return _instance;
            }
        }

        private static T _instance;

        public virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                if (transform.parent == null)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                // Must be a duplicate, destroy it.
                Destroy(gameObject);
            }
        }
    }
}
