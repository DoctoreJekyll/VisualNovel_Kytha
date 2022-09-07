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
    [Range(0.01f, 0.05f)] public float speedText;

    [Header("Choices Stuffs")]
    [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
    [SerializeField] private Button buttonPrefab;

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
            //DisplayNextLine();
        }

    }


    void LoadStory()
    {
        story = new Story(inkJsonFile.text);
        story.BindExternalFunction("Name", (string charName) => ChangeName(charName));//Muestra nombre en la caja de nombre
        story.BindExternalFunction("Scene", (string sceneN) => LoadScene(sceneN));//Carga otra escena
        story.BindExternalFunction("Position", (float x, float y) => ChangePosition(x, y));//NO FUNCIONA PÈRO debería cambiar la posicion de los personajes
        story.BindExternalFunction("Enter", (string pjName) => Enter(pjName));//Introduce un personaje en pantalla, por ahora en el centro de la misma
        story.BindExternalFunction("Exit", (string pjName) => Exit(pjName));//Saca un personaje de pantalla
        story.BindExternalFunction("SetPosition", (string dataPos, float x, float y) => SetPosition(dataPos, x, y));//NO FUNCIONA PERO debería marcar una posicion inicial de los pj
        story.BindExternalFunction("Chapter", (string chapter) => LoadOtherInk(chapter));//Esto es para cambiar entre ink archivos
        story.BindExternalFunction("SetLayer", (string layer) => SetLayerImage(layer, BCFC.instance.foreground));//NO FUNCIONA PERO debería de cambiar los fondos

    }


    public void DisplayNextLine()
    {
        if (story.canContinue)
        {
            string text = story.Continue();
            text = text?.Trim();
            dialogueBox.text = text;
        }
        else if (story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
        else
        {
            dialogueBox.text = "END";//Temporal
        }
    }

    private void DisplayChoices()
    {

        Debug.Log("test");

        if (verticalLayoutGroup.GetComponentsInChildren<Button>().Length > 0)
        {
            return;
        }

        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            Choice choice = story.currentChoices[i];
            Button button = CreateChoiceButton(choice.text);

            button.onClick.AddListener(() => OnClickChoiceButton(choice));
        }

    }

    private Button CreateChoiceButton(string text)
    {
        Button choiceButton = Instantiate(buttonPrefab);
        choiceButton.transform.SetParent(verticalLayoutGroup.transform, false);

        TMP_Text buttonTxt = choiceButton.GetComponentInChildren<TMP_Text>();
        buttonTxt.text = text;

        return choiceButton;

    }

    private void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshChoiceView();
        ShowChar();

    }

    private void RefreshChoiceView()
    {
        if (verticalLayoutGroup != null)
        {
            foreach (var button in verticalLayoutGroup.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
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
        else if (story.currentChoices.Count > 0)
        {
            DisplayChoices();
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


    void SetPosition(string data, float locationX, float locationY)
    {

        string[] parameters = data.Split(',');
        string character = parameters[0];
        locationX = float.Parse(parameters[1], System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"));
        locationY = parameters.Length == 3 ? float.Parse(parameters[2], System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US")) : 0;

        Character c = CharacterManager.instance.GetCharacters(character);
        c.SetPosition(new Vector2(locationX, locationY));

        print("set " + c.characterName + " position to " + locationX + "," + locationY);
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

    public void LoadOtherInk(string name)
    {
        TextAsset temp;
        Story tempStory;

        temp = (TextAsset)Resources.Load("InkArchive/Ink[" + name + "]");

        tempStory = new Story(temp.text);
        story = tempStory;

        //story = new Story(inkJsonFile.text);

    }

    void SetLayerImage(string data, BCFC.LAYER layer)
    {
        string texName = data.Contains(",") ? data.Split(',')[0] : data;
        Texture2D tex = texName == "null" ? null : Resources.Load("Art/Images/UI/Backdrops/" + texName) as Texture2D;
        float spd = 2f;
        bool smooth = false;

        if (data.Contains(","))
        {
            string[] parameters = data.Split(',');
            foreach (string p in parameters)
            {
                float fVal = 0;
                bool bVal = false;
                if (float.TryParse(p, out fVal))
                {
                    spd = fVal; continue;
                }
                if (bool.TryParse(p, out bVal))
                {
                    smooth = bVal; continue;
                }
            }
        }

        layer.TransitionToTexture(tex, spd, smooth);
    }
    //(Resources.Load("Characters/Character[" + characterName + "]") != null)
    //    [Header("Dialogue Stuffs")]
    //public TextAsset inkJsonFile;
    //public Story story;


}
