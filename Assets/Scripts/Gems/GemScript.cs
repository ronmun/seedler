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
            GameManager.instance.AddGems(1);
            other.gameObject.GetComponent<PlayerMovement>().gems += 1;
            collectible.PlaySound();
            gameObject.SetActive(false);
        }
    }
}
