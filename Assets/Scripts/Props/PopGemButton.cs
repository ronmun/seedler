using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopGemButton : MonoBehaviour
{
    public GameObject diamond;

    private void Start()
    {
        diamond.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !diamond.activeInHierarchy)
        {
            diamond.SetActive(true);
        }
    }
}
