/// <summary>
/// 2018/12/31 
/// Singleton Class
/// </summary>

public class Singleton<T> where T : class, new()
{
    static T _instance;

    static object _lock = new object();

    public static T Instance
    {
        get
        {

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
    }
}