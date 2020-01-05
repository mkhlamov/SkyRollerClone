using UnityEngine;

namespace SkyRollerClone
{
    public class Singleton<T> : MonoBehaviour where T: Singleton<T>
    {
        private static T _Instance;

        public static T Instance
        {
            get
            {
                return _Instance;
            }
        }

        public static bool IsInitialized
        {
            get
            {
                return _Instance != null;
            }
        }

        protected virtual void Awake()
        {
            if (_Instance != null)
            {
                DestroyImmediate(this);
            } else
            {
                _Instance = (T)this;
            }
        }

        protected virtual void OnDestroy()
        {
            if (_Instance == this)
            {
                _Instance = null;
            }
        }
    }
}
