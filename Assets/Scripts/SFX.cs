using UnityEngine;

public class SFX : MonoBehaviour
{
    public static SFX S;

    private void Awake()
    {
        if (S == null)
            S = this;
        else if (S != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public AudioClip[] clips;
    private AudioSource[] _au;
    
    private int _auIdx = 0;

    void Start()
    {
        _au = GetComponents<AudioSource>();
    }

    public void PlaySFX(int index)
    {
        _au[_auIdx].clip = clips[index];
        _au[_auIdx].Play();
        _auIdx++;
        if (_auIdx >= _au.Length)
        {
            _auIdx -= _au.Length;
        }
    }
}