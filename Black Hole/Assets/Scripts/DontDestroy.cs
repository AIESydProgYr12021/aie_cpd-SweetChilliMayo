using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    static DontDestroy instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }            
    }
}
