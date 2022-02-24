using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{
    [Header("Dialogue Stuffs")]
    public TextAsset inkJsonFile;
    public Story story;

    public TMP_Text dialogueBox;
    public TMP_Text nameTag;

    [Header("Dialogue Charact")]
    [Range(0.01f,0.05f)]public float speedText;

    private void Start()
    {
        LoadStory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
            StartCoroutine(ShowChar());
        }
    }


    void LoadStory()
    {
        story = new Story(inkJsonFile.text);
        story.BindExternalFunction("Name", (string charName) => ChangeName(charName));
        story.BindExternalFunction("Scene", (string sceneN) => LoadScene(sceneN));
        story.BindExternalFunction("Position", (float x, float y) => ChangePosition(x, y));
        story.BindExternalFunction("Enter", (string pjName) => Enter(pjName));
        story.BindExternalFunction("Exit", (string pjName) => Exit(pjName));
        story.BindExternalFunction("SetPosition", (string dataPos, float x, float y) => SetPosition(dataPos, x, y));

    }


    public void DisplayNextLine()
    {
        if (story.canContinue)
        {
            string text = story.Continue();
            text = text?.Trim();
            dialogueBox.text = text;
        }
        else
        {
            dialogueBox.text = "END";//Temporal
        }
    }

    public IEnumerator ShowChar()
    {
        if (story.canContinue)
        {
            string text = story.Continue();
            text = text?.Trim();

            dialogueBox.text = "";
            foreach (char c in text)
            {
                dialogueBox.text += c;
                yield return new WaitForSeconds(speedText);
            }


        }
        else
        {
            dialogueBox.text = "END";//Temporal
        }
    }



    public void Enter(string data)
    {
        string[] parameters = data.Split(',');
        string[] characters = parameters[0].Split(';');
        float speed = 3;
        bool smooth = false;
        for (int i = 1; i < parameters.Length; i++)
        {
            float fVal = 0; bool bVal = false;
            if (float.TryParse(parameters[i], out fVal))
            { speed = fVal; continue; }
            if (bool.TryParse(parameters[i], out bVal))
            { smooth = bVal; continue; }
        }

        foreach (string s in characters)
        {
            Character c = CharacterManager.instance.GetCharacters(s, true, false);
            c.enabled = true;
            c.FadeIn(speed, smooth);
        }
    }

    void Exit(string data)
    {
        string[] parameters = data.Split(',');
        string[] characters = parameters[0].Split(';');
        float speed = 3;
        bool smooth = false;
        for (int i = 1; i < parameters.Length; i++)
        {
            float fVal = 0; bool bVal = false;
            if (float.TryParse(parameters[i], out fVal))
            { speed = fVal; continue; }
            if (bool.TryParse(parameters[i], out bVal))
            { smooth = bVal; continue; }
        }

        foreach (string s in characters)
        {
            Character c = CharacterManager.instance.GetCharacters(s);
            c.FadeOut(speed, smooth);
        }
    }


    public void ChangeName(string name)
    {
        string speakerName = name;

        nameTag.text = speakerName;
    }


    void SetPosition(string name, float x, float y)
    {

        string character = name;
        float locationX = x;
        float locationY = y;

        Character c = CharacterManager.instance.GetCharacters(character);
        c.SetPosition(new Vector2(locationX, locationY));

        Debug.Log("set " + c.characterName + " position to " + locationX + "," + locationY);
    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }



    public void ChangePosition(float x, float y)
    {
        Vector2 newPosition = new Vector2(x, y);
        Debug.Log(newPosition);
    }

}
