using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;

    public float gravity = 9.81f;

    public float jumpSpeed = 5f;

    public float rotationSpeed = 90f;

    private float directionY;

    public Transform player;

    public bool turned = false;

    // Update is called once per frame
    void Update()
    {

        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(-vertical, 0f, horizontal).normalized; 
        direction.y = directionY;

        //character go brrrr & turning
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }
       
        //jumping
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            directionY = jumpSpeed;
        }

        directionY -= gravity * Time.deltaTime;

    }


}
