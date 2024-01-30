using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseFile : MonoBehaviour
{

    public Culprit culpritGuess;
    public CauseOfDeath causeOfDeathGuess;

    public Case thisCase;


    void Awake()
    {
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCulpritGuess()
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
        //spawn all the culprit/cause of death photos listed in the Case
    }

    void OnTriggerEnter(Collider other)
    {
        Interactable_Picture interactable = other.GetComponent<Interactable_Picture>();
        if(interactable != null)
        {
            if(interactable.culpritPhoto)
            {
                culpritGuess = interactable.myCulprit;
            }
            
            if(interactable.causeOfDeathPhoto)
            {
                causeOfDeathGuess = interactable.myCauseOfDeath;
            }
        }
    }



    


}
