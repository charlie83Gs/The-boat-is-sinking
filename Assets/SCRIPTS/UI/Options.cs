using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Options : MonoBehaviour {

    public AudioMixer audioMixer;

    public void SetVolume(float pVolume)
    {
        audioMixer.SetFloat("MainVolume", pVolume);
    }

    public void setFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen; 
    }
}
