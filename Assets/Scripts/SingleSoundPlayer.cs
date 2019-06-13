using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSoundPlayer : MonoBehaviour
{
    public SoundValue sound;

    public void PlaySound()
    {
        Director.GetManager<SoundManager>().PlaySound(sound);
    }

}
