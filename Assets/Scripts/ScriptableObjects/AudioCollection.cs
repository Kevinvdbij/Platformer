using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new AudioCollection", menuName = "Data/Audio Collection", order = 0)]
public class AudioCollection : ScriptableObject
{
    public List<AudioClip> audio;

    public AudioClip GetRandom
    {
        get
        {
            return audio[Random.Range(0, audio.Count - 1)];
        }
    }

    public AudioClip GetAtIndex (int index)
    {
        if (audio[index])
        {
            return audio[index];
        }
        else
        {
            return audio[0];
        }
    }
}
