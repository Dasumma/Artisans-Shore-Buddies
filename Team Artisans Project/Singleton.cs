using UnityEngine;


public class Singleton : MonoBehaviour
{
    private static Singleton _instance;

    public static Singleton instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject obj = newGameObject;
                _instance = obj.AddComponent<Singleton>();
            
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if(_instance != null) Destroy(this);
        DontDestroyOnLoad(this);
    }
}
