using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public LocationManager currentLocation;
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

    void OnEnable()
    {
        //instance = this;
    }
    void Awake()
    {
        instance = this;

        

        LoadSave();

        currentCase = cases[0];
        
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
        }
        
    }

    public void SetNewLocation(LocationManager location)
    {
        currentLocation = location;


        if(currentView == null)
        {
            currentView = currentLocation.defaultView;
        }

        PlayerInput.instance.UpdateBackplane(currentView.myBackPlane);
    }

    public void Travel(LocationManager destination)
    {
        
        currentLocation.gameObject.SetActive(false);
        currentLocation = destination;

        SetView(currentLocation.defaultView);
    }

    public void TrySolveCase()
    {
        currentGuess = caseFileObj.GetGuess();

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
            
            SetNewCase(cases[caseIndex]);
            caseFileObj.ResetCaseFile();
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
    public void AddMilestone(Milestone m)
    {
        if(CheckMilestone(m))
        {

        } else 
        {
            
            completedMilestones.Add(m);
            string[] temp = m.ToString().Split('_');
            string title = temp[0], id = temp[1];
            
            EvidencePopup.instance.Spawn(title, id);
        }
    }
    public void AddMilestone(string milestoneID)
    {
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
    NON_SUPERNATURAL, MOTHMAN, WEREWOLF
}
[Serializable]
public enum CauseOfDeath
{
    HEART_ATTACK, BLED_OUT, POISONED
}

[Serializable]
public enum Milestone
{

    EVIDENCE_FOOTPRINTS, EVIDENCE_FEATHERS, EVIDENCE_C1_3, EVIDENCE_C1_4, EVIDENCE_C1_5, 
    EVIDENCE_C2_1, EVIDENCE_C2_2, EVIDENCE_C2_3, EVIDENCE_C2_4, EVIDENCE_C2_5, 
    EVIDENCE_C3_1, EVIDENCE_C3_2, EVIDENCE_C3_3, EVIDENCE_C3_4, EVIDENCE_C3_5, 
    EVIDENCE_C4_1, EVIDENCE_C4_2, EVIDENCE_C4_3, EVIDENCE_C4_4, EVIDENCE_C4_5, 
    EVIDENCE_C5_1, EVIDENCE_C5_2, EVIDENCE_C5_3, EVIDENCE_C5_4, EVIDENCE_C5_5, 
    ITEM_PUZZLE,
    CS1_DONE, CS2_DONE, CS3_DONE, CS4_DONE, CS5_DONE



}