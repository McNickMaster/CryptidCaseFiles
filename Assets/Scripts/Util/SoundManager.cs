using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource musicSource;
    public AudioSource sfxSource;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaySFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void PlaySFX_Delay(AudioClip clip, float delay)
    {
        sfxSource.clip = clip;
        Invoke("PlaySFXSource",delay);

    }

    private void PlaySFXSource()
    {
        sfxSource.Play();
    }
}
