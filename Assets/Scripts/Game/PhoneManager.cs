using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{

    public static PhoneManager instance;

    private SimpleTextData caseResult;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Trigger_CaseSolved(Case myCase, bool correct)
    {
        if(correct)
        {
            caseResult = myCase.correctResult;
        } else 
        {
            caseResult = myCase.incorrectResult;
        }
        SpawnPhoneCall();
    }

    public void Trigger_CaseSolved(Case myCase, bool correct, float delay)
    {
        if(correct)
        {
            caseResult = (myCase.correctResult);
        } else 
        {
            caseResult = (myCase.incorrectResult);
        }
        
        Invoke("SpawnPhoneCall",delay);
    }

    void SpawnPhoneCall()
    {
        DialogueLoader.instance.LoadPhonecall(""+caseResult.name);
        
    }

    //void Spawn


}
