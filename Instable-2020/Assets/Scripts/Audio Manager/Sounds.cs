using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Sounds
{
    public string name;

    public AudioClip Audio;

    [Range(0f, 1f)]
    public float Volume;

    [Range(.3f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource Source;

    public AudioMixerGroup Output;

    public bool loop = false;

    public bool play = false;

}
