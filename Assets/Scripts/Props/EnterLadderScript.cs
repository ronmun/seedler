using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLadderScript : MonoBehaviour
{
    public LadderScript ladder;
    public string direction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!other.GetComponent<PlayerMovement>().canClimb)
                ladder.SendList(direction);
        }
    }
}
