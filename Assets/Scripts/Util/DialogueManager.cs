using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;


    public LoadTextFromJson jsonLoader; 
    public GameObject dialogueBase;
    TextMeshProUGUI title, body;

    private DialogueScriptable dialogueData;
    private GameObject objInstance;

    private int dialogueIndex = 0, numPages = 0;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnDialogue(DialogueScriptable dialogueData)
    {
        dialogueIndex = 0;
        this.dialogueData = dialogueData;

        PlayerInput.instance.enabled = false;


        objInstance = Instantiate(dialogueBase, Vector3.zero, Quaternion.identity, transform.GetChild(0).transform);
        objInstance.transform.localPosition = Vector3.zero;
        title = objInstance.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        body = objInstance.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();

        Button btn = objInstance.GetComponent<Button>();
        btn.onClick.AddListener(NextPage);

        numPages = dialogueData.numPages;
        jsonLoader.LoadJson(dialogueData.GetDialoguePage(0));

        title.text = jsonLoader.GetNotePages()[0];
        body.text = jsonLoader.GetNotePages()[1];


    }

    public void NextPage()
    {
        dialogueIndex++;

        if(dialogueIndex >= numPages)
        {
            Destroy(objInstance);
            PlayerInput.instance.enabled = true;

        } else 
        {
            
            jsonLoader.LoadJson(dialogueData.GetDialoguePage(dialogueIndex));

            title.text = jsonLoader.GetNotePages()[0];
            body.text = jsonLoader.GetNotePages()[1];

        }
    }
}
