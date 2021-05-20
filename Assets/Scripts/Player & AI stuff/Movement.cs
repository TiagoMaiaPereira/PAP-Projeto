using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f; //Velocidade do jogador

    public Transform player;

    public int coins = 0;

    public TextMeshProUGUI textCoins;

    public TextMeshProUGUI lvl;

    FOVDetection fov;

    public GameObject gameOver;

    public Animator anim;

    public int currentLvl;

    public bool inGameOver = false;

    private void Start()
    {
        currentLvl = SceneManager.GetActiveScene().buildIndex;
        lvl.text = currentLvl.ToString();
        Time.timeScale = 1f;
        fov = FindObjectOfType<FOVDetection>();
        gameOver.SetActive(false);
        controller.enabled = true;
        coins = 0;
    }

    // Update is called once per frame
    void Update()
    {

        float vertical = Input.GetAxisRaw("Vertical"); //comando que apanha o Input vertical (W e S)
        float horizontal = Input.GetAxisRaw("Horizontal"); //comando que apanha o Input horizontal (A e D)
        Vector3 direction = new Vector3(-vertical, 0f, horizontal).normalized; //indica a direção da entidade do jogador

        //faz o movimento e rotação do jogador
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
            anim.SetFloat("Speed", speed);
        } else if (direction.magnitude <= 0f)
        {
            anim.SetFloat("Speed", 0);
        }


        
        if(fov.isInFOV == true)
        {
            inGameOver = true;
            gameOver.SetActive(true);
            Time.timeScale = 0f;
        }

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
