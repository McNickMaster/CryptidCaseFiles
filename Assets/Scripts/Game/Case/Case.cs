using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CaseData", menuName = "ScriptableObjects/CaseData", order = 1)]
public class Case : ScriptableObject
{
    public Culprit culprit;
    public CauseOfDeath causeOfDeath;

    public List<Culprit> culpritList = new List<Culprit>();
    
    public void Init(Culprit culp, CauseOfDeath cause)
    {
        culprit = culp;
        causeOfDeath = cause;
    }


    public bool SolveCase(Case guess)
    {
        bool culp = guess.culprit == this.culprit;
        bool cause = guess.causeOfDeath == this.causeOfDeath;

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
