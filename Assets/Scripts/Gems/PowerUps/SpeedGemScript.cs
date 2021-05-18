using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGemScript : MonoBehaviour
{
    public GameObject Model;
    public float speed;
    private CollectibleScript collectible;

    private void Awake()
    {
        collectible = GetComponentInParent<CollectibleScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(other.gameObject.GetComponent<PlayerMovement>().SpeedBoost(speed));
            collectible.PlaySound();
            gameObject.GetComponent<BoxCollider>().enabled = false;
            Model.SetActive(false);
        }
    }
}
