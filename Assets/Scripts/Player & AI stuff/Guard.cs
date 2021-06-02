using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Guard : MonoBehaviour
{
    public Transform player;
    public float AIMoveSpeed = 5f; //velocidade de base
    public float damping;

    public GameObject destPoints;
    public List<Transform> navPoint;
    public NavMeshAgent agent;
    public int destPoint = 1;
    public Transform goal;
    FOVDetection fov;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        navPoint = destPoints.GetComponentsInChildren<Transform>().ToList();
        navPoint.RemoveAt(0);
        fov = GetComponent<FOVDetection>();
        agent.autoBraking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 0.5f) //Mede a distância ao ponto seguinte
        {
            GoToNextPoint();
        }

        if (fov.isInFOV) //apenas para teste
        {
            player.GetComponent<Movement>().GotCaught(); //referência ao jogador para indicar que foi detetado
            LookAtPlayer();
            Chase();
        }
    }

    void GoToNextPoint() //move a entidade para o ponto seguinte
    {

        if (navPoint.Count == 0)
            return;


        agent.destination = navPoint[destPoint].position;
        destPoint++;
        if(destPoint >= navPoint.Count)
        {
            destPoint = 0;
        }
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