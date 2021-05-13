using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    public List<Transform> List;
    private PlayerMovement player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    public void SendList(string direction)
    {
        player.GetComponent<Rigidbody>().useGravity = false;

        player.stairPoints = List;

        if (direction == "up")
            player.stairIndex = 0;
        else
            player.stairIndex = List.Count-1;

        player.stairRotation = gameObject.transform.forward;

        player.canClimb = true;
    }
}
