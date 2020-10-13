using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundSetting : MonoBehaviour
{
    public AudioMixer MainMixer;

    public Slider Master_Volume,SFX_Volume,Music_Volume;

    public TextMeshProUGUI Master_txt, SFX_txt, Music_txt;

    private void Start()
    {
        SetOnStart();
    }

    public void SetOnStart()
    {
        if (PlayerPrefs.HasKey("Master_Vol"))
        {
            MainMixer.SetFloat("Master", PlayerPrefs.GetFloat("Master_Vol"));

            Master_Volume.value = PlayerPrefs.GetFloat("Master_Vol");

            Master_txt.text = (Master_Volume.value + 80f).ToString();
        }
        if (PlayerPrefs.HasKey("Music_Vol"))
        {
            MainMixer.SetFloat("Music", PlayerPrefs.GetFloat("Music_Vol"));

            Music_Volume.value = PlayerPrefs.GetFloat("Music_Vol");

            Music_txt.text = (Music_Volume.value + 80f).ToString();
        }
        if (PlayerPrefs.HasKey("SFX_Vol"))
        {
            MainMixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFX_Vol"));

            SFX_Volume.value = PlayerPrefs.GetFloat("SFX_Vol");

            SFX_txt.text = (SFX_Volume.value + 80f).ToString();
        }
    }

    public void SetMasterVol()
    {
        Master_txt.text = (Master_Volume.value + 80f).ToString();

        MainMixer.SetFloat("Master", Master_Volume.value);

        PlayerPrefs.SetFloat("Master_Vol", Master_Volume.value);
    }

    public void SetMusicVol()
    {
        Music_txt.text = (Music_Volume.value + 80f).ToString();

        MainMixer.SetFloat("Music", Music_Volume.value);

        PlayerPrefs.SetFloat("Music_Vol", Music_Volume.value);
    }

    public void SetSFXVol()
    {
        SFX_txt.text = (SFX_Volume.value + 80f).ToString();

        MainMixer.SetFloat("SFX", SFX_Volume.value);

        PlayerPrefs.SetFloat("SFX_Vol", SFX_Volume.value);
    }

    public void DefaultAudioSetting()
    {

        MainMixer.SetFloat("Master", -5f);

        Master_Volume.value = -5f;

        Master_txt.text = (Master_Volume.value + 80f).ToString();

        MainMixer.SetFloat("Music", 0f);

        Music_Volume.value = 0f;

        Music_txt.text = (Music_Volume.value + 80f).ToString();

        MainMixer.SetFloat("SFX", -3f);

        SFX_Volume.value = -3f;

        SFX_txt.text = (SFX_Volume.value + 80f).ToString();
        PlayerPrefs.SetFloat("SFX_Vol", SFX_Volume.value);
        PlayerPrefs.SetFloat("Music_Vol", Music_Volume.value);
        PlayerPrefs.SetFloat("Master_Vol", Master_Volume.value);
        PlayerPrefs.Save();
    }
}
