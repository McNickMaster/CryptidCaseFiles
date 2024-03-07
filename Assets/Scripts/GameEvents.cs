using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;
    public bool EnableTutorialDialogue = false;

    [Header("Tutorial")]
    public UnityEvent Event_EnterCrimeScene1;
    public UnityEvent Event_Evidence;
    public UnityEvent Event_NPC;
    public UnityEvent Event_EnterOffice;
    public UnityEvent Event_FirstNote;
    public UnityEvent Event_FirstCaseFile;

    private bool firstEvidence = true, firstNPC = true, firstNote = true, firstCaseFale = true;

    [Header("GameEvent")]
    public UnityEvent Event_Puzzle2_Done;
    

    [SerializeField]
    private List<string> scenesLoadedBefore = new List<string>();

    void Awake()
    {
        instance = this;


    }

    void OnEnable()
    {
        
        if (Event_EnterCrimeScene1 == null)
            Event_EnterCrimeScene1 = new UnityEvent();

        if (Event_EnterOffice == null)
            Event_EnterOffice = new UnityEvent();

        
        if (Event_Evidence == null)
            Event_Evidence = new UnityEvent();

        Event_Evidence.AddListener(CheckFirstEvidence);
        Event_NPC.AddListener(CheckFirstNPC);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scenesLoadedBefore.Contains(scene.name))
        {

        } else if(EnableTutorialDialogue)
        {
            scenesLoadedBefore.Add(scene.name);

            switch(scene.name)
            {
                case "Office":
                {
                    Debug.Log("office loaded for the first time");
                    Event_EnterOffice.Invoke();
                    break;
                }
                case "CS1":
                {
                    Event_EnterCrimeScene1.Invoke();
                    break;
                }
            }
        }
    }


    public void CheckFirstEvidence()
    {
        if(firstEvidence)
        {
            DialogueLoader.instance.LoadMonologue("Tutorial_FirstEvidence");
        } else 
        {

        }
        firstEvidence = false;
    }
    public void CheckFirstNPC()
    {
        if(firstNPC)
        {
            DialogueLoader.instance.LoadMonologue("Tutorial_FirstNPC");
        } else 
        {

        }
        firstNPC = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
