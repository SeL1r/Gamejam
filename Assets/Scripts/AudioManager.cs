using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetMasterVolume(float volume)
    {
        float volumeDB = Mathf.Log10(volume) * 20;
        if (volume <= .001f)
            volumeDB = -80f;
        mixer.SetFloat("MasterVolume", volumeDB);
    }

    public void SetMusicVolume(float volume)
    {
        float volumeDB = Mathf.Log10(volume) * 20;
        if (volume <= 0.001f)
            volumeDB = -80f;
        mixer.SetFloat("MusicVolume", volumeDB);
    }

    public void SetSFXVolume(float volume)
    {
        float volumeDB = Mathf.Log10(volume) * 20;
        if (volume <= 0.001f)
            volumeDB = -80f;
        mixer.SetFloat("SFXVolume", volumeDB);
    }
}
