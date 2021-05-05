using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    public Transform[] target;
    public int currentPatrolPoint;
    public NavMeshAgent agent;
    public Animator animator;
    private bool changing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target[currentPatrolPoint].position);

        if (agent.remainingDistance <= .1f && !changing){
            changing = true;
            currentPatrolPoint ++;
            if (currentPatrolPoint >= target.Length) {
                currentPatrolPoint = 0;
            }
            changing = false;
        }

        
    }
}
