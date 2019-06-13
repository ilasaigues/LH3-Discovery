using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName = "SoundLibrary", menuName = "Scriptables/Values/SoundLibrary")]
public class SoundLibrary : SoundValue
{
    public List<AudioClip> clips = new List<AudioClip>();

    public override AudioClip GetValue()
    {
        if (clips.Count == 0) return null;
        return clips[UnityEngine.Random.Range(0, clips.Count)];
    }
}
