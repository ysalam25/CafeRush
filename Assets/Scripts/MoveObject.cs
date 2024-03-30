using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveObject : MonoBehaviour
{
    public Transform targetPosition; 
    public float speed = 1.0f; 


    void Update()
    {
        // Move our position a step closer to the target.
        float step = speed * Time.deltaTime; 
        if (transform.position != targetPosition.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, step);
        }
    }
}
