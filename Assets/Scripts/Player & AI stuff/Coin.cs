using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Collider coin;

    public void OnCollisionEnter(Collision coin)
    {
        Debug.Log("Hit coin");
    }
}
