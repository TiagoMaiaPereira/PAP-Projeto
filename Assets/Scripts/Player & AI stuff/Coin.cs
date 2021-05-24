using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Collider coin;

    public Transform coinTrans;

    void Update()
    {
        if (transform.position.y <= 1.5f || transform.position.y >= 1.5f)
        {
            coinTrans.position = new Vector3 (transform.position.x, 1.5f, transform.position.z);
        }
    }

    public void OnCollisionEnter(Collision coin)
    {
        Debug.Log("Hit coin");
    }
}
