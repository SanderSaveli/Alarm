using UnityEngine;

public abstract class Singletone<T> : MonoBehaviour where T : Component
{
    private static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameObject = new GameObject('[' + typeof(T).Name + ']');
                _instance = gameObject.AddComponent<T>();
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning($"Singletone of type {typeof(T)} already available!");
            Destroy(gameObject);
        }
        if (_instance == null)
        {
            _instance = gameObject.GetComponent<T>();
        }
    }

    public virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}