using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Collider tutorial;

    public GameObject tutorialPrompt;

    void OnTriggerEnter(Collider tutorial)
    {
        tutorialPrompt.SetActive(true);
        Time.timeScale = 0f;
    }

}
