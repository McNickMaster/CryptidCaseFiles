using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    [Header("Modules")]
    public GameData gameData;
    public Transform playerUIParent;
    public SceneLoadHelper sceneLoad;
    public DialogueLoader dialogueLoader;
    public PhonecallManager phonecallManager;
    public Cutscene travelCutscene;
    public Cutscene endCaseCutscene;
    public GameObject lineDrawer;
    public GameObject winScreen, loseScreen;
    public CaseFile caseFileObj;
    public GameObject evidencePopPrefab;

    [Header("Data")]
    public e_Scene startScene = e_Scene.OFFICE;
    public e_Scene currentScene;
    public View currentView;
    public View[] views;
    public Case[] cases;

    [Header("Instances")]
    public Case currentCase;
    public LocationManager currentLocation;
    private Case currentGuess;
    private int caseIndex;

    [Header("Debug")]
    public List<Milestone> completedMilestones = new List<Milestone>();

    public UnityEvent event_StartGameLoad = new UnityEvent();
    public bool loadSave = false;
    public bool loadFirstScene = true;

    void Awake()
    {
        instance = this;

        
        
        
        //currentLocation = FindObjectOfType<LocationManager>();
       
    }
    
    void OnEnable()
    {
        //instance = this;

        if(loadSave)
        {
            LoadSave();
        }
        

        if(loadFirstScene)
        {
            sceneLoad.LoadFirstScene(startScene.ToString());
            currentScene = startScene;
        }

        SetNewCase(cases[0]);
        currentCase.Setup();

    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetWinLoss();
        }*/
        if(Input.GetKeyDown(KeyCode.F))
        {
           // SaveGame();
           //Setup();
           //Travel(e_Scene.OFFICE);
        }
        
    }
/*
    public void SetNewLocation(LocationManager location)
    {
        currentLocation = location;


        if(currentView == null)
        {
            currentView = currentLocation.defaultView;
        }

        PlayerInput.instance.UpdateBackplane(currentView.myBackPlane);
    }
    */

    public void SetNewLocation(e_Scene newScene)
    {
        
        SceneLoadHelper.instance.LoadNewScene(SceneManager.GetSceneByName(currentScene.ToString()), 
            SceneManager.GetSceneByName(newScene.ToString()));
        
        currentScene = newScene;

    }

    public void SetNewView(View view)
    {
        
        if(currentView == null)
        {
            currentView = currentLocation.defaultView;
        }

        PlayerInput.instance.UpdateBackplane(currentView.myBackPlane);
    }

    
    public void TravelOffice()
    {
        
        Travel(e_Scene.OFFICE); 
    }

    public void Travel(LocationManager destination)
    {
        
        //currentLocation.gameObject.SetActive(false);
        //currentLocation = destination;

        SetView(currentLocation.defaultView);
    }

    public void Travel(e_Scene destination_enum)
    {
        travelCutscene.StartCutscene();
        sceneLoad.LoadNewScene(destination_enum.ToString());
        currentScene = destination_enum;
    }

    public void TravelToDest()
    {
        travelCutscene.StartCutscene();
        e_Scene destination;

        if(currentScene.ToString().Equals("OFFICE"))
        {
            destination = currentCase.crimeScene;
        } else 
        {
            destination = e_Scene.OFFICE;
        }

        sceneLoad.LoadNewScene(destination.ToString());
        currentScene = destination;
    }

    public void TrySolveCase()
    {
        currentGuess = CaseFile.instance.GetGuess();

        

       // GameEvents.instance.Event_SolveCase.Invoke();
        NextCase();

    }


    void SetNewCase(Case newCase)
    {
        currentCase = newCase;
    }

    void NextCase()
    {
        caseIndex++;

        if(caseIndex>=cases.Length)
        {

        } else 
        {
            bool caseSolved = currentCase.SolveCase(currentGuess);
            Case oldCase = currentCase;

            SetNewCase(cases[caseIndex]);
            CaseFile.instance.ResetCaseFile();
            currentCase.Setup();
            CaseFile.instance.SetCase(currentCase);
            
            endCaseCutscene.StartCutscene();
            sceneLoad.LoadNewScene("OFFICE");
            currentScene = e_Scene.OFFICE;
            GameEvents.instance.Event_SolveCase.Invoke();
            PhoneManager.instance.Trigger_CaseSolved(oldCase, caseSolved, 0.5f);
        }
    }

    public void ResetWinLoss()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    public DrawLine SpawnLineDrawer()
    {
        return Instantiate(lineDrawer, Vector3.zero, Quaternion.identity, currentLocation.transform).GetComponent<DrawLine>();
    }

    public void SetView(int i)
    {
        currentView.enabled = false;
        currentView = views[i];
        currentView.enabled = true;
    }

    public void SetView(View view)
    {
        currentView.enabled = false;
        currentView = view;
        currentView.enabled = true;
    }

    public bool CheckMilestone(Milestone m)
    {
        return completedMilestones.Contains(m);
    }

    int popCount = 0;
    public void AddMilestone(Milestone m)
    {
        if(CheckMilestone(m))
        {

        } else 
        {
            
            completedMilestones.Add(m);
            string[] temp = m.ToString().Split('_');
            string title = temp[0], id = temp[1];

            
            
            switch(title)
            {
                case "EVIDENCE":
                {
                    
                    title = "Evidence";
                    currentCase.evidenceList.Add(m);
                    break;
                }

                case "CAUSE":
                {
                    currentCase.causeOfDeathList.Add(Enum.Parse<CauseOfDeath>(id));
                    Debug.Log("parsing: " +id);
                    title = "Cause Of Death";
                    break;
                }
                case "CULP":
                {
                    currentCase.culpritList.Add(Enum.Parse<Culprit>(id));
                    title = "Culprit";
                    break;
                }
                case "VICTIM":
                {
                    currentCase.victimList.Add(Enum.Parse<Victim>(id));
                    title = "Victim";
                    break;
                }
            }

            if(id.Contains('x'))
            {
                Debug.Log("found the x in: " + id);
                id = id.Replace('x', ' ');
                Debug.Log("id after replacing: " + id);
            }

            EvidencePopup pop = Instantiate(evidencePopPrefab, new Vector3(25,-25,0), Quaternion.identity, playerUIParent).GetComponent<EvidencePopup>();
            pop.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(25,-25,0);
            pop.Spawn(title, id);
            pop.transform.SetAsFirstSibling();
        }
    }
    
    public void AddMilestone(string milestoneID)
    {
        Debug.Log("adding milestone: " + milestoneID);
        AddMilestone(Enum.Parse<Milestone>(milestoneID));
    }
    
    
    void SaveGame()
    {
        SaveLoadData.SaveData(this);
    }

    void LoadSave()
    {
        try{
            
            SaveData mySave = SaveLoadData.LoadData();
            completedMilestones = mySave.GetMilestones();

        } catch (Exception e)
        {
            Debug.Log("save file not found");
        }
    }

    void OnDisable()
    {
        //SaveGame();
        completedMilestones = null;
    }

}

[Serializable]
public enum Culprit
{
    NULL, MOTHMAN, WEREWOLF, KRAKEN, MAHAMBA, SKEL
}
[Serializable]
public enum CauseOfDeath
{
    NULL, HEARTATTACK, BLEED, POISONED
}
[Serializable]
public enum Victim
{
    NULL, NPC1, NPC2, NPC3, NPC4, NPC5
}

[Serializable]
public enum Milestone
{

    EVIDENCE_FOOTPRINTS, EVIDENCE_FLECKS, EVIDENCE_C1_3, EVIDENCE_C1_4, EVIDENCE_C1_5, 
    EVIDENCE_PUZZLExCYPHER, EVIDENCE_CLAWMARKS, EVIDENCE_CARGOxMANIFEST, EVIDENCE_C2_4, EVIDENCE_C2_5, 
    EVIDENCE_C3_1, EVIDENCE_C3_2, EVIDENCE_C3_3, EVIDENCE_C3_4, EVIDENCE_C3_5, 
    EVIDENCE_C4_1, EVIDENCE_C4_2, EVIDENCE_C4_3, EVIDENCE_C4_4, EVIDENCE_C4_5, 
    EVIDENCE_C5_1, EVIDENCE_C5_2, EVIDENCE_C5_3, EVIDENCE_C5_4, EVIDENCE_C5_5, 
    CAUSE_BLEED, CAUSE_HEARTATTACK, CULP_MOTHMAN, CULP_WEREWOLF,
    ITEM_PUZZLE,
    CS1_DONE, CS2_DONE, CS3_DONE, CS4_DONE, CS5_DONE,
    CULP_KRAKEN, CULP_MAHAMBA, CULP_SKEL, 
    VICTIM_NPC1, VICTIM_NPC2, VICTIM_NPC3, VICTIM_NPC4, VICTIM_NPC5,

    ITEM_CARGO, ITEM_5, ITEM_6, ITEM_7, ITEM_8, ITEM_9, ITEM_10, ITEM_11,
    PUZZLE2_DONE, PUZZLE3_DONE, PUZZLE4_DONE, PUZZLE5_DONE



}