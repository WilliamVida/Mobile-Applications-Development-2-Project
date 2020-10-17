using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// https://www.youtube.com/watch?v=zc8ac_qUXQY
public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
