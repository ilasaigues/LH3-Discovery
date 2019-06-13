using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SoundManager : Manager
{
    public AudioMixer master;
    public AudioMixerGroup defaultSoundGroup;
    private List<AudioSource> _usedSources = new List<AudioSource>();
    private Dictionary<SoundValue, AudioSource> _permanentSources = new Dictionary<SoundValue, AudioSource>();
    private List<AudioSource> _freeSources = new List<AudioSource>();

    public float Volume { get; private set; } = 1;

    private void Start()
    {
        Volume = PlayerPrefs.GetFloat("Volume", 1);
        SetMasterVolumeScalar(Volume);
    }

    public AudioSource PlaySound(SoundValue sound)
    {
        AudioSource source;
        if (_freeSources.Count == 0)
        {
            source = new GameObject().AddComponent<AudioSource>();
            source.transform.parent = transform;
        }
        else
        {
            source = _freeSources[0];
            _freeSources.RemoveAt(0);
        }
        source.gameObject.name = "Sound: " + sound.name;
        AssignSoundToSource(source, sound);
        source.Play();
        return source;
    }

    public void StopSound(SoundValue sound)
    {
        if (_permanentSources.ContainsKey(sound))
        {
            Destroy(_permanentSources[sound].gameObject);
            _permanentSources.Remove(sound);
        }
    }

    public void SetMasterVolumeDB(float vol)
    {
        master.SetFloat("MasterVolume", vol);
    }

    public void SetMasterVolumeScalar(float vol)
    {
        SetMasterVolumeDB((Mathf.Sqrt(vol) - 1) * 80);
        PlayerPrefs.SetFloat("Volume_Master", vol);
        Volume = vol;
    }

    public void SetMusicVolumeDB(float vol)
    {
        master.SetFloat("MusicVolume", vol);
    }

    public void SetMusicVolumeScalar(float vol)
    {
        SetMusicVolumeDB((Mathf.Sqrt(vol) - 1) * 80);
        PlayerPrefs.SetFloat("Volume_Music", vol);
        Volume = vol;
    }

    public void SetFXVolumeDB(float vol)
    {
        master.SetFloat("FXVolume", vol);
    }

    public void SetFXVolumeScalar(float vol)
    {
        SetFXVolumeDB((Mathf.Sqrt(vol) - 1) * 80);
        PlayerPrefs.SetFloat("Volume_FX", vol);
        Volume = vol;
    }
    void AssignSoundToSource(AudioSource source, SoundValue sound)
    {
        if (sound.loop)
        {
            if (_permanentSources.ContainsKey(sound))
            {
                Destroy(source.gameObject);
                source = _permanentSources[sound];
            }
            else
            {
                _permanentSources[sound] = source;
            }
        }
        else _usedSources.Add(source);

        source.clip = sound.GetValue();
        source.volume = sound.volume;
        source.loop = sound.loop;
        source.pitch = sound.pitch;
        if (sound.mixerGroup == null) source.outputAudioMixerGroup = defaultSoundGroup;
        else source.outputAudioMixerGroup = sound.mixerGroup;
    }

    private void Update()
    {
        for (int i = _usedSources.Count - 1; i >= 0; i--)
        {
            if (!_usedSources[i].isPlaying)
            {
                _usedSources[i].gameObject.name = "[Free AudioSource]";
                _freeSources.Add(_usedSources[i]);
                _usedSources.RemoveAt(i);

            }
        }
    }

    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }
}