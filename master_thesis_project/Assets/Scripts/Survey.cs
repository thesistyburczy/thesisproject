using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class Survey : MonoBehaviour
{
    [Header("Form Fields")]

    [Header("General")]

    [Tooltip("Age")]
    public ToggleGroup age;
    private string ageString;

    [Tooltip("Gender")]
    public ToggleGroup gender;
    private string genderString;

    [Tooltip("Origin")]
    public ToggleGroup origin;
    private string originString;

    [Tooltip("Game Experience")]
    public ToggleGroup experience;
    private string experienceString;

    [Tooltip("Game Experience")]
    public ToggleGroup favColor;
    private string favColorString;

    [Header("Story")]

    [Tooltip("Interaction Black Knight")]
    public ToggleGroup choicesLady;
    private string choicesLadyString;
    
    [Tooltip("Interaction Black Knight")]
    public ToggleGroup choicesDragon;
    private string choicesDragonString;
    
    [Tooltip("Interaction Black Knight")]
    public Toggle[] characteristicsBlackKnight = new Toggle[11];
    private string characteristicsBlackKnightString;
    
    [Tooltip("Interaction Black Knight")]
    public Toggle[] characteristicsDragon = new Toggle[11];
    private string characteristicsDragonString;

    [Tooltip("Interaction Black Knight")]
    public ToggleGroup blackKnight;
    private string blackKnightString;

    [Tooltip("Interaction Lady")]
    public ToggleGroup lady;
    private string ladyString;

    [Header("Satisfaction")]
    [Tooltip("Result Satisfaction")]
    public ToggleGroup resultSatisfaction;
    private string resultSatisfactionString;

    [Tooltip("Reading Flow")]
    public ToggleGroup readingflow;
    private string readingString;

    [Tooltip("Prediction")]
    public ToggleGroup prediction;
    private string predictionString;

    private int continueProgress = 0;

    [Header("Sections")]
    [SerializeField] private GameObject welcomeSection;

    [SerializeField] private GameObject generalSection1;
    [SerializeField] private GameObject generalSection2;
    // answers question
    [SerializeField] private GameObject storySection;
    // characteristics
    [SerializeField] private GameObject storySection1;
    [SerializeField] private GameObject storySection2;
    // enocunter feeling
    [SerializeField] private GameObject storySection3;
    [SerializeField] private GameObject storySection4;

    [SerializeField] private GameObject satisfactionSection1;
    [SerializeField] private GameObject satisfactionSection2;

    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject continueButton;

    private void Start()
    {
        //deactivate all screns that are currently not visible
        welcomeSection.SetActive(true);

        generalSection1.SetActive(false);
        generalSection2.SetActive(false);

        storySection.SetActive(false);
        storySection1.SetActive(false);
        storySection2.SetActive(false);
        storySection3.SetActive(false);
        storySection4.SetActive(false);

        satisfactionSection1.SetActive(false);
        satisfactionSection2.SetActive(false);

        endScreen.SetActive(false);
    }

    // read and store data from all toggles that will be saved later
    public void readFromFields()
    {
        generalSection1.SetActive(true);
        generalSection2.SetActive(true);

        storySection.SetActive(true);
        storySection1.SetActive(true);
        storySection2.SetActive(true);
        storySection3.SetActive(true);
        storySection4.SetActive(true);

        satisfactionSection1.SetActive(true);
        satisfactionSection2.SetActive(true);

        foreach (Toggle toggle in age.ActiveToggles())
        {
            ageString = toggle.GetComponentInChildren<Text>().text;
        }

        foreach (Toggle toggle in gender.ActiveToggles())
        {
            genderString = toggle.GetComponentInChildren<Text>().text;
        }

        foreach (Toggle toggle in origin.ActiveToggles())
        {
            originString = toggle.GetComponentInChildren<Text>().text;
        }

        foreach (Toggle toggle in experience.ActiveToggles())
        {
            experienceString = toggle.GetComponentInChildren<Text>().text;
        }

        foreach (Toggle toggle in favColor.ActiveToggles())
        {
            favColorString = toggle.GetComponentInChildren<Text>().text;
        }

        foreach (Toggle toggle in choicesLady.ActiveToggles())
        {
            choicesLadyString = toggle.GetComponentInChildren<Text>().text;
        }

        foreach (Toggle toggle in choicesDragon.ActiveToggles())
        {
            choicesDragonString = toggle.GetComponentInChildren<Text>().text;
        }

        foreach (Toggle toggle in characteristicsBlackKnight)
        {
            if (toggle.isOn)
            { 
                characteristicsBlackKnightString += toggle.GetComponentInChildren<Text>().text + ", ";
            }
        }

        foreach (Toggle toggle in characteristicsDragon)
        {
            if (toggle.isOn)
            { 
                characteristicsDragonString += toggle.GetComponentInChildren<Text>().text + ", ";
            }
        }
        
        foreach (Toggle toggle in blackKnight.ActiveToggles())
        {
            blackKnightString = toggle.GetComponentInChildren<Text>().text;
        }

        foreach (Toggle toggle in lady.ActiveToggles())
        {
            ladyString = toggle.GetComponentInChildren<Text>().text;
        }

        foreach (Toggle toggle in resultSatisfaction.ActiveToggles())
        {
            resultSatisfactionString = toggle.GetComponentInChildren<Text>().text;
        }

        foreach (Toggle toggle in readingflow.ActiveToggles())
        {
            readingString = toggle.GetComponentInChildren<Text>().text;
        }

        foreach (Toggle toggle in prediction.ActiveToggles())
        {
            predictionString = toggle.GetComponentInChildren<Text>().text;
        }
    }

    // url for survey, removed to protect privacy
    string baseUrl = "";
    string baseUrlColor = "";

    public void Send()
    {
        StartCoroutine(Post());
    }

    // send the data to the correct fields, field names removed to protect privacy
    IEnumerator Post()
    {
        WWWForm form = new WWWForm();
        // Add fields to form
        form.AddField("", ageString);
        form.AddField("", genderString);
        form.AddField("", originString);
        form.AddField("", experienceString);
        form.AddField("", favColorString);
        form.AddField("", choicesLadyString);
        form.AddField("", choicesDragonString);
        form.AddField("e", characteristicsBlackKnightString);
        form.AddField("", characteristicsDragonString);
        form.AddField("", blackKnightString);
        form.AddField("", ladyString);
        form.AddField("", resultSatisfactionString);
        form.AddField("", readingString);
        form.AddField("", predictionString);

        form.AddField("", Timer.GetInstance().GetTime(1).ToString());
        form.AddField("", Timer.GetInstance().GetTime(2).ToString());
        form.AddField("", Timer.GetInstance().GetTime(3).ToString());
        form.AddField("", Timer.GetInstance().GetTime(4).ToString());
        form.AddField("", Timer.GetInstance().GetTime(5).ToString());
        form.AddField("", Timer.GetInstance().GetTime(6).ToString());
        form.AddField("", Timer.GetInstance().GetFinalTime().ToString());

        form.AddField("", DialogueManager.GetInstance().GetFinalChoice(1).ToString());
        form.AddField("", DialogueManager.GetInstance().GetFinalChoice(2).ToString());
        form.AddField("", DialogueManager.GetInstance().GetFinalChoice(3).ToString());
        form.AddField("", DialogueManager.GetInstance().GetFinalChoice(4).ToString());
        form.AddField("", DialogueManager.GetInstance().GetFinalChoice(5).ToString());
        form.AddField("", DialogueManager.GetInstance().GetFinalChoice(6).ToString());

        UnityWebRequest www;

        if (DialogueManager.GetInstance().color)
        {
            www = UnityWebRequest.Post(baseUrlColor, form);
        }
        else { 
            www = UnityWebRequest.Post(baseUrl, form);
        }

        yield return www.SendWebRequest();
    }

    // screens get activated and deactivated by continuing
    public void ContinueButton()
    {
        switch (continueProgress)
        {
            case 0:
                welcomeSection.SetActive(false);
                generalSection1.SetActive(true);
                continueProgress++;
                break;
            case 1:
                generalSection1.SetActive(false);
                generalSection2.SetActive(true);
                continueProgress++;
                break;
            case 2:
                generalSection2.SetActive(false);
                storySection.SetActive(true);
                continueProgress++;
                break;
            case 3:
                storySection.SetActive(false);
                storySection1.SetActive(true);
                continueProgress++;
                break;
            case 4:
                storySection1.SetActive(false);
                storySection2.SetActive(true);
                continueProgress++;
                break;
            case 5:
                storySection2.SetActive(false);
                storySection3.SetActive(true);
                continueProgress++;
                break;
            case 6:
                storySection3.SetActive(false);
                storySection4.SetActive(true);
                continueProgress++;
                break;
            case 7:
                storySection4.SetActive(false);
                satisfactionSection1.SetActive(true);
                continueProgress++;
                break;
            case 8:
                satisfactionSection1.SetActive(false);
                satisfactionSection2.SetActive(true);
                continueProgress++;
                break;
            case 9:
                satisfactionSection2.SetActive(false);
                endScreen.SetActive(true);
                readFromFields();
                Send();
                continueButton.SetActive(false);
                break;
            default:
                break;
        }
    }


}
