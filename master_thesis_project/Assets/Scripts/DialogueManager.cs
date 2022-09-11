using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

// inspired by https://www.youtube.com/watch?v=vY0Sk93YUhA
public class DialogueManager : MonoBehaviour
{
    [Header("Ink JSON")]
    public TextAsset inkJSON;
    
    public TextAsset inkJSONColor;

    private TextAsset inkStory;
    public bool color = true;

    [Header("Dialogue UI")]

    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]

    [SerializeField] private GameObject[] choices;

    private TextMeshProUGUI[] choicesText;

    public Story currentStory;

    public bool dialogueIsPlaying { get; private set; }

    [SerializeField] private GameObject survey;

    private List<int> finalChoices = new List<int>();

    #region Singleton-instance

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }

        if (Random.value > 0.35)
        {
            inkStory = inkJSONColor;
            color = true;
        }
        else 
        {
            inkStory = inkJSON;
            color = false;
        }
    }

    public static DialogueManager GetInstance() 
    {
        return instance;
    }

    public TextAsset GetStory()
    {
        return inkStory;
    }

    #endregion

    private void Start()
    {
        dialogueIsPlaying = false;

        // get all choices
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        // return if there is no dialogue to be played
        if (!dialogueIsPlaying)
        {
            return;
        }

        // handle input here
        if (Input.GetMouseButtonDown(0) && currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }
    }

    // start the story
    public void EnterDialogue(TextAsset inkJSON) 
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;

        ContinueStory();
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for current dialogue line
            dialogueText.text = currentStory.Continue();
            // display choices for current dialogue
            DisplayChoices();
        }
        else
        {
            ExitDialogue();
        }
    }

    // end the story
    private void ExitDialogue()
    {
        dialogueIsPlaying = false;
        dialogueText.text = "";
        survey.SetActive(true);
        Timer.GetInstance().StopTime();
    }

    private void DisplayChoices()
    {
        Timer.GetInstance().StartLapTime();
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices given than the UI can support. Choices given: " + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and hide them
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }
    }

    // select the choice pressed by the user
    public void MakeChoice(int choiceIndex) 
    {
        currentStory.ChooseChoiceIndex(choiceIndex);

        Timer.GetInstance().SetTime(Timer.GetInstance().GetLapTime());
        print(Timer.GetInstance().GetLapTime());
        finalChoices.Add(choiceIndex);

        ContinueStory();
    }

    public float GetFinalChoice(int index)
    {
        return finalChoices[index];
    }
}
