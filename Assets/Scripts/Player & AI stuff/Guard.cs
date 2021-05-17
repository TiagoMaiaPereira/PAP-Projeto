using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform player;
    public float AIMoveSpeed = 5f; //velocidade de base
    public float damping;

    public Transform[] navPoint;
    public UnityEngine.AI.NavMeshAgent agent;
    public int destPoint = 0;
    public Transform goal;
    FOVDetection fov;

    // Start is called before the first frame update
    void Start()
    {
        fov = FindObjectOfType<FOVDetection>();

        UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.5f) //Mede a distância ao ponto seguinte
        {
            GoToNextPoint();
        }

        if (fov.isInFOV == true)
        {
            LookAtPlayer();
            Chase();
        }
    }

    void GoToNextPoint() //move a entidade para o ponto seguinte
    {

        if (navPoint.Length == 0)
            return;

        agent.destination = navPoint[destPoint].position;
        destPoint = (destPoint + 1) % navPoint.Length;
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