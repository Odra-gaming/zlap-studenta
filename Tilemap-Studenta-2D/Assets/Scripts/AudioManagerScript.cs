using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManagerScript : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManagerScript instance;

    void Awake()
    {
        if( instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake=s.onAwake;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name );
        if(s==null)
        {
            Debug.LogWarning("Soound: "+name+" not found");
            return;
        }
        //s.source.Play(); 
        switch(name)
        {
            case "Hamster": 
                s.source.loop=true;
                s.loop=true;
                s.source.playOnAwake=false;
                s.onAwake=false;
                s.source.Play(); 
                break;
            default:
                s.source.Play(); 
                break;
        }

    }
}
