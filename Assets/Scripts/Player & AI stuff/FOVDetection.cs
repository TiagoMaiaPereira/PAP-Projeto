using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOVDetection : MonoBehaviour
{
    public Transform player;
    public float maxAngle;
    public float maxRadius;

    public bool isInFOV = false;

    private void OnDrawGizmos()
    {
        //Draws the yellow detection sphere around the guard
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);

        //Define as linhas para o ponto de vista
        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        //Desenha as linhas do ponto de vista
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        //Changes the color of the ray pointing towards the player (Raycast line)
        if(!isInFOV)
        Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;
        
        Gizmos.DrawRay(transform.position, (player.position - transform.position).normalized * maxRadius); //Actually draws the Ray

        //Desenha a linha que aponta para a frente do guarda
        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position, transform.forward * maxRadius);
    }

    public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        Collider[] overlaps = new Collider[60]; //Deteta todas as colisões dentro do círculo amarelo
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        for(int i = 0; i < count + 1; i++) //Para cada colisão detetada
        {
            if(overlaps[i] != null) //Verifica se o vetor está vazio
            {
                if(overlaps[i].transform == target) //Checks if the overlaped colliders coincide with the player's collider location
                {
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                    if(angle <= maxAngle) //Checks if the raycast's angle is less or equal to the max angle (if the player is in fov or not)
                    {
                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position); //Sends a "Ray" towards the player
                        RaycastHit hit;

                        if(Physics.Raycast(ray, out hit, maxRadius)) //if the raycast is "in fov" it returns true
                        {
                            if(hit.transform == target)
                            return true;
                        }
                    }

                }
            }
        }
        

        return false;
    }

    void Update()
    {
        isInFOV = inFOV(transform, player, maxAngle, maxRadius);
    }

}


