using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.canClimb = true;
            other.attachedRigidbody.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            player.canClimb = false;
            other.attachedRigidbody.useGravity = true;
        }
    }
}
