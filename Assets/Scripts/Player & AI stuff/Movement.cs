using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f; //Velocidade do jogador

    public float gravity = 9.81f; //força da gravidade aplicada à entidade para que volte ao chão

    public float jumpSpeed = 5f; //Indica a alturta do Salto

    private float directionY; //para isolar o eixo Y

    public Transform player;

    public int coins = 0;

    public TextMeshProUGUI textCoins;


    // Update is called once per frame
    void Update()
    {

        float vertical = Input.GetAxisRaw("Vertical"); //comando que apanha o Input vertical (W e S)
        float horizontal = Input.GetAxisRaw("Horizontal"); //comando que apanha o Input horizontal (A e D)
        Vector3 direction = new Vector3(-vertical, 0f, horizontal).normalized; //indica a direção da entidade do jogador
        direction.y = directionY;

        //faz o movimento e rotação do jogador
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }

        //salto
        if (Input.GetButtonDown("Jump") && controller.isGrounded) //aplica a velocidade de salto quando o botão correspondente a salto (Barra de espaço) é premido
        {
            directionY = jumpSpeed;
        }

        directionY -= gravity * Time.deltaTime; //aplica a força de gravidade à entidade uma vez no ar

    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.gameObject.tag == "coin")
        {
            coins++;
            textCoins.text = coins.ToString();
            Destroy(hit.gameObject);
        }
    }
}
