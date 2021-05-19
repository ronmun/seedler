using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public AudioSource audio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            HealthManager.instance.Hurt();
            audio.Play();
        }
    }
}
