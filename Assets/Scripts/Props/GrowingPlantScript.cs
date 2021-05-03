using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingPlantScript : MonoBehaviour
{
    public List<Transform> points;

    private float speed = 2.0f;
    private float distance = 0.2f;

    private int nextPoint = 0;
    private float platformReady = 0;
    private float platformCooldown = 4f;

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position,
            points[nextPoint].transform.position,
            speed * Time.deltaTime);

        if (Vector3.Distance(this.transform.position, points[nextPoint].transform.position) < this.distance)
        {
            if (Time.time > platformReady)
            {
                platformReady = Time.time + platformCooldown;
                this.nextPoint++;
                if (this.nextPoint >= points.Count)
                {
                    this.nextPoint = 0;
                }
            }
                
        }
    }
}
