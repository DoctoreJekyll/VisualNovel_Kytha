using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{
    [Header("Dialogue Stuffs")]
    public TextAsset inkJsonFile;
    [SerializeField] private Story story;

    public TMP_Text dialogueBox;
    public TMP_Text nameTag;

    [Header("Dialogue Character")]
    [Range(0.01f, 0.05f)] public float speedText;

    [Header("Choices Stuffs")]
    [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
    [SerializeField] private Button buttonPrefab;

    [Header(("Others"))] 
    private BCFC.LAYER layer;
    private BCFC bgController;
    [SerializeField] private Texture[] _texture;

    private Coroutine textCorroutine;

    private void Start()
    {
        LoadStory();

        bgController = BCFC.instance;
        layer = null;
        layer = bgController.background;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //DisplayNextLine();
            //StopCoroutine(_enumerator);
            //StartCoroutine(_enumerator);
            
            ContinueStory();
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
        story.BindExternalFunction("Chapter", (string chapter) => LoadOtherInk(chapter));//Esto es para cambiar entre ink archivos
        story.BindExternalFunction("CallSetBg", (int arrayPos) => CallSetBg(arrayPos));//NO FUNCIONA PERO debería de cambiar los fondos
        story.BindExternalFunction("SetPositionTest", (string pjName, float amount) => SetPositionTest(pjName, amount));//Mueve un personaje a partir de su nombre y un valor que será la posicion en X
        story.BindExternalFunction("MoveCharacter", (string namePj, float locationX, float speed) => MoveCharacter(namePj, locationX, speed));

        //MoveCharacter(string namePj, float locationX, float locationY, float speed, bool smooth)

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
        StartCoroutine(ShowTextLetterByLetter(story.Continue()));

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

    private void ContinueStory()
    {
        if (story.canContinue)
        {
            if (textCorroutine != null)
            {
                StopCoroutine(textCorroutine);
            }
            textCorroutine = StartCoroutine(ShowTextLetterByLetter(story.Continue()));
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

    private IEnumerator ShowTextLetterByLetter(string line)
    {
        dialogueBox.text = "";

        foreach (var t in line.ToCharArray())
        {
            dialogueBox.text += t;
            yield return new WaitForSeconds(speedText);
        }
    }
    

    private void Enter(string data)
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

    private void Exit(string data)
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
    
    private void ChangeName(string namePj)
    {
        string speakerName = namePj;

        nameTag.text = speakerName;
    }
    
    private void SetPositionTest(string pjName, float amount)
    {
        GameObject objToMove = GameObject.Find("Character" + "[" + pjName + "]" + "(Clone)");
        RectTransform actualPosInCanvas = objToMove.GetComponent<RectTransform>();

        actualPosInCanvas.localPosition = new Vector3(amount, 0, 0);
        
    }

    private void MoveCharacter(string namePj, float locationX, float speed)
    {
        StartCoroutine(MoveCharacterCoroutine(namePj, locationX, speed));
    }

    private IEnumerator MoveCharacterCoroutine(string namePj, float locationX, float speed)
    {
        GameObject objToMove = GameObject.Find("Character" + "[" + namePj + "]" + "(Clone)");
        
        
        RectTransform actualPosInCanvas = objToMove.GetComponent<RectTransform>();
        Vector2 target = new Vector2(locationX, 0f);

        while (actualPosInCanvas.anchoredPosition != target)
        {
            actualPosInCanvas.anchoredPosition = Vector2.MoveTowards(actualPosInCanvas.anchoredPosition, target, speed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

    }
    
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    

    private void ChangePosition(float x, float y)
    {
        Vector2 newPosition = new Vector2(x, y);
        Debug.Log(newPosition);
    }

    private void LoadOtherInk(string nameString)
    {
        inkJsonFile = (TextAsset)Resources.Load("InkArchive/Ink[" + nameString + "]");

        Story tempStory = new Story(inkJsonFile.text);
        story = tempStory;
        
        LoadStory();
        //story = new Story(inkJsonFile.text);

    }

    private void CallSetBg(int  arrayPos)
    {
        layer.TransitionToTexture(_texture[arrayPos], 1f, false);
    }
    
    private void SetLayerImage(string data, BCFC.LAYER layer)
    {
        Debug.Log(("Enter in setlayerimage"));
        string texName = data;
        Texture2D tex = texName == "null" ? null : Resources.Load("Art/Images/UI/Backdrops/" + texName) as Texture2D;
        float spd = 2f;
        bool smooth = false;
        
        // string[] parameters = data.Split(',');
        // foreach (string p in parameters)
        // {
        //     float fVal = 0;
        //     bool bVal = false;
        //     if (float.TryParse(p, out fVal))
        //     {
        //         spd = fVal; continue;
        //     }
        //     if (bool.TryParse(p, out bVal))
        //     {
        //         smooth = bVal; continue;
        //     }
        // }

        layer.TransitionToTexture(tex, spd, smooth);
    }
    //(Resources.Load("Characters/Character[" + characterName + "]") != null)
    //    [Header("Dialogue Stuffs")]
    //public TextAsset inkJsonFile;
    //public Story story;


}
