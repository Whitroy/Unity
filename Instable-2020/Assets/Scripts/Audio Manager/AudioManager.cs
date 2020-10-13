using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static AudioManager instance;

    public AudioMixer MainMixer;

    private List<Sounds> soundPlayed=new List<Sounds>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach (Sounds s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Audio;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.pitch;
            s.Source.loop = s.loop;
            s.Source.outputAudioMixerGroup = s.Output;
        }

    }

    public void Play(string name)
    {
        foreach (Sounds s in sounds)
        {
            if (s.name == name)
            {
                s.Source.Play();
                s.play = true;
                break;
            }
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("Master_Vol"))
        {
            MainMixer.SetFloat("Master", PlayerPrefs.GetFloat("Master_Vol"));
        }
        else
        {
            MainMixer.SetFloat("Master",20);
        }
        if (PlayerPrefs.HasKey("Music_Vol"))
        {
            MainMixer.SetFloat("Music", PlayerPrefs.GetFloat("Music_Vol"));
        }
        else
        {
            MainMixer.SetFloat("Music", 0);
        }
        if (PlayerPrefs.HasKey("SFX_Vol"))
        {
            MainMixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX_Vol"));
        }
        else
        {
            MainMixer.SetFloat("SFX", 3);
        }
    }
    public void Stop(string name)
    {
        foreach (Sounds s in sounds)
        {
            if (s.name == name)
            {
                s.Source.Stop();
                s.play = false;
                break;
            }
        }
    }

    public void PlayALL()
    {
        foreach (Sounds s in soundPlayed)
        {
            s.Source.Play();
            s.play = true;
        }
        soundPlayed.Clear();
    }

    public void StopALL()
    {
        foreach(Sounds s in  sounds)
        {
            if(s.play==true)
            {
                soundPlayed.Add(s);
            }
        }

        foreach(Sounds s in soundPlayed)
        {
            s.Source.Stop();
            s.play = false;
        }
    }
}