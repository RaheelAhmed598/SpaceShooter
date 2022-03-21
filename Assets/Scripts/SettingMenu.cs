using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void Volume(float volume )
    {
        Debug.Log("Volume up and down system");
        audioMixer.SetFloat("Volume", volume);
    }
}
