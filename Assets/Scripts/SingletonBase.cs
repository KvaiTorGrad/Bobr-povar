using UnityEngine;

namespace SingleTon
{
    [DisallowMultipleComponent]
    public abstract class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private bool _doNotDestroyOnLoad;
        
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this as T;
            if (_doNotDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
    }
}