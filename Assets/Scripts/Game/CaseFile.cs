using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseFile : MonoBehaviour
{

    public Culprit culpritGuess;
    public CauseOfDeath causeOfDeathGuess;

    public Case thisCase;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    
    public Case GetGuess()
    {
        Case caseGuess = ScriptableObject.CreateInstance<Case>();
        caseGuess.Init(culpritGuess, causeOfDeathGuess);
        return caseGuess;
    }

    public void SetCase()
    {

    }

    public void PopulateCulprits()
    {
        
    }

    


}
