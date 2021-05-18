using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleScript : MonoBehaviour
{
    public AudioSource audio;

    public void PlaySound()
    {
        audio.Play();
    }
}
