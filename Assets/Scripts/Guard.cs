using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform player;
    public bool playerSeen; //Indica se o jogador é visto ou não
    public float AIMoveSpeed = 5f; //velocidade de base
    public float damping;
    public Collider view; //"Campo de visão""

    public Transform[] navPoint;
    public UnityEngine.AI.NavMeshAgent agent;
    public int destPoint = 0;
    public Transform goal;


    // Start is called before the first frame update
    void Start()
    {
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

        if (playerSeen == true)
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

    void OnTriggerEnter(Collider view) //Ativa quando o jogador entra no "campo de visão"
    {
        if(view.tag == "player")
        {
            playerSeen = true;
        }
    }

    void OnTriggerExit(Collider view) //Ativa quando o jogador sai do "campo de visão"
    {
        if (view.tag == "player")
        {
            playerSeen = false;
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