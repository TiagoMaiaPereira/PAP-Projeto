using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform player;
    public bool playerSeen;
    public float AIMoveSpeed = 5f;
    public float damping;
    public Collider view;

    public Transform[] navPoint;
    public UnityEngine.AI.NavMeshAgent agent;
    public int destPoint = 0;
    public Transform goal;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.destination = goal.position;

        agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerSeen == true)
        {
            LookAtPlayer();
            Chase();
        }
        else
        {
            GoToNextPoint();
        }
        if (agent.remainingDistance < 0.5f)
        {
            GoToNextPoint();
        }

    }

    void GoToNextPoint()
    {
        if (navPoint.Length == 0)
        {
            agent.destination = navPoint[destPoint].position;
            destPoint = (destPoint + 1) % navPoint.Length;
        }
    }

    void OnTriggerEnter(Collider view)
    {
        playerSeen = true;
    }

    void OnTriggerExit(Collider view)
    {
        playerSeen = false;
    }
    void LookAtPlayer() //for testing purposes
    {
        transform.LookAt(player);

    }

    void Chase()
    {
        transform.Translate(Vector3.forward * AIMoveSpeed * Time.deltaTime);
    }
}