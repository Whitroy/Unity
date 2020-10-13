using UnityEngine;
using UnityEngine.Audio;
[System.Serializable]
public class Audio
{
    public string name;

    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float Volume;

    [Range(.3f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource Source;

    public bool loop;
}
