using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance=null;

    public List<Audio> audios = new List<Audio>();
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
            return;
        }

        foreach(Audio audio in audios)
        {
            audio.Source = gameObject.AddComponent<AudioSource>();
            audio.Source.volume = audio.Volume;
            audio.Source.pitch=audio.pitch;
            audio.Source.clip = audio.audioClip;
            audio.Source.name = audio.name;
            audio.Source.loop = audio.loop;
        }
    }


    public void Play(string name)
    {
        foreach (Audio s in audios)
        {
            if (s.name == name)
            {
                s.Source.Play();
                break;
            }
        }
    }


    public void Stop(string name)
    {
        foreach (Audio s in audios)
        {
            if (s.name == name)
            {
                s.Source.Stop();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Audio audio in audios)
        {
            audio.Source.volume = audio.Volume;
            audio.Source.pitch = audio.pitch;
        }
    }

    public void VolumeControl(bool status)
    {
        if(status)
        {
            foreach (Audio s in audios)
            {
                if (s.name == "Music")
                {
                    s.Volume = 0f;
                    break;
                }
            }
        }
        else
        {
            foreach (Audio s in audios)
            {
                if (s.name == "Music")
                {
                    s.Volume = 0.8f;
                    break;
                }
            }
        }
    }
}
