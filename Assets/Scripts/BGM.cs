using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM S;

    private void Awake()
    {
        if (S == null)
            S = this;
        else if (S != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}