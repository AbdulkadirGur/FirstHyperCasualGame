using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager2 : MonoBehaviour
{
    public static SoundManager2 instance;
    private AudioSource audioSource;
    public bool sound;
    private void Awake()
    {
        makeSingleton();
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
    private void makeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }


    public void soundOnOff()
    {
        sound  = !sound;
    }
    public void playSoundFX(AudioClip clip,float volume)
    {
        if (sound)
        {
            audioSource.PlayOneShot(clip, volume); 
        }
    }
}
