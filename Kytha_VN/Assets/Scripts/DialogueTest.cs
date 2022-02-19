using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueTest : MonoBehaviour
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



    public void ChangeName(string name)
    {
        string speakerName = name;

        nameTag.text = speakerName;
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
