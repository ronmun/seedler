using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    private CollectibleScript collectible;

    private void Awake()
    {
        collectible = GetComponentInParent<CollectibleScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().gems += 1;
            collectible.PlaySound();
            gameObject.active = false;
        }
    }
}
