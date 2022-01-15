using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeValueChange : MonoBehaviour
{
    public GameObject[] soundFX;

    void Start()
    {
        soundFX = GameObject.FindGameObjectsWithTag("SoundFX");
    }
    public void volumeChange()
    {
        Level.volume = gameObject.GetComponent<Slider>().value;
        foreach (GameObject soundfx in soundFX)
        {
            soundfx.gameObject.GetComponent<AudioSource>().volume = Level.volume;
        }
    }

}
