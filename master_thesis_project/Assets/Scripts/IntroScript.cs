using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject introScreen;
    [SerializeField] private GameObject tutorialScreen;

    [SerializeField] private Toggle dsgvoToggle;
    [SerializeField] private Button continueButton;

    private bool firstPress = false;

    void Start()
    {
        // deactivate everything except startscreen on start of game
        introScreen.SetActive(false);
        tutorialScreen.SetActive(false);
        continueButton.interactable = false;
    }

    void Update()
    {
        // checks for first click to switch away from startscreen
        if (Input.GetMouseButtonDown(0) && !firstPress)
        {
            firstPress = true;
            startScreen.SetActive(false);
            introScreen.SetActive(true);
        }

        // user can proceed when dsgvo toggle is checked
        if (dsgvoToggle.isOn)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void StartTutorial()
    {
        // starts the tutorial/story and timer
        introScreen.SetActive(false);
        startScreen.SetActive(false);
        tutorialScreen.SetActive(true);
        DialogueManager.GetInstance().EnterDialogue(DialogueManager.GetInstance().GetStory());
        Timer.GetInstance().StartTime();
    }
}
