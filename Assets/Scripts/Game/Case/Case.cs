using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CaseData", menuName = "ScriptableObjects/CaseData", order = 1)]
public class Case : ScriptableObject
{
    public Culprit culprit;
    public CauseOfDeath causeOfDeath;
    public Victim victim;

    public e_Scene crimeScene;

    public SimpleTextData correctResult, incorrectResult;

    public List<Culprit> culpritList = new List<Culprit>();
    public List<CauseOfDeath> causeOfDeathList = new List<CauseOfDeath>();
    public List<Victim> victimList = new List<Victim>();
    public List<Milestone> evidenceList = new List<Milestone>();

    public List<DialogueData> phoneCallList;

    private bool culpSolved, victSolved, causeSolved;
    
    public void Setup()
    {
        
        //culpritList = new List<Culprit>();
        //causeOfDeathList = new List<CauseOfDeath>();

        //what case am I? and reset lists accoordingly
        switch(crimeScene)
        {
            case e_Scene.CS1:
            {
                culpritList = new List<Culprit>();
                causeOfDeathList = new List<CauseOfDeath>();
                evidenceList = new List<Milestone>();
                break;
            }
            case e_Scene.CS2:
            {
                culpritList = new List<Culprit>();
                victimList = new List<Victim>();
                evidenceList = new List<Milestone>();
                break;
            }

        }
        
    }

  
    public void InitGuess(Culprit culp, CauseOfDeath cause, Victim victim)
    {
        culprit = culp;
        causeOfDeath = cause;
        victim = victim;
    }


    public bool SolveCase(Case guess)
    {
        bool culp = guess.culprit == this.culprit;
        bool cause = guess.causeOfDeath == this.causeOfDeath;
        bool vict = guess.victim == this.victim;

        if(culp && cause)
        {
            return true;
        } 

        return false;
    }

    public Culprit[] GetCulpritList()
    {
        return culpritList.ToArray();
    }


}
