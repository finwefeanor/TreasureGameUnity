using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolScript : MonoBehaviour
{
    public Transform[] wayPoints;
    private int destinationPoints = 0;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).

        //agent.autoBraking = false;

        GoToNextPoint();
        
    }

    private void GoToNextPoint()
    {
        // Returns if no points have been set up
        if (wayPoints.Length == 0)
        {
            return;
        }
        // Set the agent to go to the currently selected destination.
        agent.destination = wayPoints[destinationPoints].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        //For loop effect. 5 % 5  is 0 and it will start over for again for loop.
        destinationPoints = (destinationPoints + 1) % wayPoints.Length;
    }

    // Update is called once per frame
    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }
        
    }
}
