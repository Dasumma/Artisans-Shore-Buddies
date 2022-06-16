public class Singleton : MonoBehaviour
{
    private static Singleton _instance;

    public static Singleton instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject().AddComponent<Singleton>();
            
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