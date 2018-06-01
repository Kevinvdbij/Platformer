using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSystem : MonoBehaviour
{
    public AudioSource source;
    public AudioClip pickup;
    public AudioClip jump;
    public AudioClip attack;
    public AudioClip death;

    public void PlayPowerUpSound()
    {
        source.clip = pickup ;
        source.Play();
    }
}
