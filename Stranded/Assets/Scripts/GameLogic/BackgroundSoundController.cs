using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundController : MonoBehaviour
{
    public float MinVol;
    public float MaxVol;
    public float UpdateFrequency;
    AudioSource Audio;
    float Timer;
    float VolumeSet = 0.15f;

    void Awake() {
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        
        if(Timer >= UpdateFrequency) {
            Timer = 0;
            // Randomize new volume
            VolumeSet = Random.Range(MinVol * 100, MaxVol * 100) / 100;
        }

        // Adjust volume gradually
        if(Audio.volume < VolumeSet) {
            // Set Volume
            Audio.volume = Audio.volume + Time.deltaTime / 50;
        }
        if(Audio.volume > VolumeSet) {
            // Set Volume
            Audio.volume = Audio.volume - Time.deltaTime / 50;
        }
    }
}
