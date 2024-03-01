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
    public SceneLoadHelper sceneLoad;
    public DialogueLoader dialogueLoader;
    public LocationManager currentLocation;
    public e_Scene startScene = e_Scene.OFFICE;
    public e_Scene currentScene;
    public Cutscene travelCutscene;
    public View currentView;
    public View[] views;
    public GameObject lineDrawer;
    public GameObject winScreen, loseScreen;
    public CaseFile caseFileObj;
    public Case[] cases;
    public Case currentCase;
    private Case currentGuess;
    private int caseIndex;

    [Header("Debug")]
    public List<Milestone> completedMilestones = new List<Milestone>();

    public UnityEvent event_StartGameLoad = new UnityEvent();
    public bool loadFirstScene = true;

    void OnEnable()
    {
        //instance = this;
    }
    void Awake()
    {
        instance = this;

        

        LoadSave();

        if(loadFirstScene)
        {
            sceneLoad.LoadFirstScene(startScene.ToString());
            currentScene = startScene;
        }

        currentCase = cases[0];
        currentCase.Setup();

        //Invoke("LoadStartingDialogue", 0.5f);
        
        //currentLocation = FindObjectOfType<LocationManager>();
       
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

        PhoneManager.instance.Trigger_CaseSolved(currentCase.SolveCase(currentGuess));

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
            
            //SetNewCase(cases[caseIndex]);
            //CaseFile.instance.ResetCaseFile();
            //currentCase.Setup();
            //CaseFile.instance.SetCase(currentCase);
        }
    }

    
    public void LoadStartingDialogue()
    {
       dialogueLoader.LoadMonologue(""+gameData.startingPhonecall.order);
       dialogueLoader.StartMonologue();
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

                    title = "Culprit";
                    break;
                }
            }
            EvidencePopup.instance.Spawn(title, id);
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

}

[Serializable]
public enum Culprit
{
    NONSUPERNATURAL, MOTHMAN, WEREWOLF, KRAKEN, MAHAMBA, SKEL
}
[Serializable]
public enum CauseOfDeath
{
    HEARTATTACK, BLEED, POISONED, NPC1, NPC2, NPC3, NPC4, NPC5
}

[Serializable]
public enum Milestone
{

    EVIDENCE_FOOTPRINTS, EVIDENCE_FEATHERS, EVIDENCE_C1_3, EVIDENCE_C1_4, EVIDENCE_C1_5, 
    EVIDENCE_C2_1, EVIDENCE_C2_2, EVIDENCE_C2_3, EVIDENCE_C2_4, EVIDENCE_C2_5, 
    EVIDENCE_C3_1, EVIDENCE_C3_2, EVIDENCE_C3_3, EVIDENCE_C3_4, EVIDENCE_C3_5, 
    EVIDENCE_C4_1, EVIDENCE_C4_2, EVIDENCE_C4_3, EVIDENCE_C4_4, EVIDENCE_C4_5, 
    EVIDENCE_C5_1, EVIDENCE_C5_2, EVIDENCE_C5_3, EVIDENCE_C5_4, EVIDENCE_C5_5, 
    CAUSE_BLEED, CAUSE_HEARTATTACK, CULP_MOTHMAN, CULP_WEREWOLF,
    ITEM_PUZZLE,
    CS1_DONE, CS2_DONE, CS3_DONE, CS4_DONE, CS5_DONE,
    CULP_KRAKEN, CULP_MAHAMBA, CULP_SKEL, 
    CAUSE_NPC1, CAUSE_NPC2, CAUSE_NPC3, CAUSE_NPC4, CAUSE_NPC5



}