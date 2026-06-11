using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour, IDataPersist
{
    public AudioMixer mixer;

    public AudioSource sfxEnemyDestroy;
    public AudioSource bgmMain;

    void Start()
    {
        //Bind our audio effect to the enemy destroy event
        Enemy.onEnemyDestroy += sfxEnemyDestroy.Play;
    }

    void OnDestroy()
    {
        //Remove our binding if this object is destroyed
        Enemy.onEnemyDestroy -= sfxEnemyDestroy.Play;
    }

    public void AdjustVolume(Slider slider)
    {
        //Set the mixer's volume based on the slider name + value
        mixer.SetFloat(slider.name, slider.value);
    }

    public void AdjustVolume(string name, float value)
    {
        //Directly set the volume for a named channel
        mixer.SetFloat(name, value);
    }

    public void Save()
    {
        //Save our current volumes (triggered by IDataPersist)
        SaveVolume("VolumeMaster");
        SaveVolume("VolumeMusic");
        SaveVolume("VolumeSFX");
    }

    public void Load()
    {
        //Load the saved volumes (triggered by IDataPersist)
        LoadVolume("VolumeMaster");
        LoadVolume("VolumeMusic");
        LoadVolume("VolumeSFX");
    }

    private void SaveVolume(string name)
    {
        //Get the volume from the mixer, and save that value to PlayerPrefs
        float f = GetCurrentVolume(name);
        PlayerPrefs.SetFloat(name, f);
    }

    private void LoadVolume(string name)
    {
        //Get a float from PlayerPrefs and set it as our audio value in the mixer
        float value = PlayerPrefs.GetFloat(name);
        AdjustVolume(name, value);
    }

    private float GetCurrentVolume(string name)
    {
        //If we find the mixer channel requested, return the volume
        if (mixer.GetFloat(name, out float vol))
            return vol;

        //else return a default of 1
        return 1;
    }
}
