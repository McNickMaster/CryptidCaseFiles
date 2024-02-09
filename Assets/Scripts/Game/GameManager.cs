using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private Case currentCase;
    private Case currentGuess;
    private int caseIndex;

    [Header("Debug")]
    public List<Milestone> completedMilestones = new List<Milestone>();

    void Awake()
    {
        instance = this;

        currentCase = cases[0];

        if(currentView == null)
        {
            currentView = currentLocation.defaultView;
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.F))
        {
            //TrySolveCase();
        }
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


       if(currentCase.SolveCase(currentGuess))
       {
            //NextCase();
            Win();
       } else 
       {
            Lose();
       }
    }

    void SetNewCase(Case newCase)
    {
        currentCase = newCase;
    }

    void NextCase()
    {
        caseIndex++;
//        SetNewCase(cases[caseIndex]);
    }

    public void Win()
    {
        winScreen.SetActive(true);
    }

    public void Lose()
    {
        loseScreen.SetActive(true);
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
    public void CompleteMilestone(Milestone m)
    {
        if(CheckMilestone(m))
        {

        } else 
        {
            completedMilestones.Add(m);
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

public enum Milestone
{

    PUZZLE_FOUND



}