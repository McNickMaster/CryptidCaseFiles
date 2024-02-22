using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{

    public static PhoneManager instance;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Trigger_CaseSolved(bool correct)
    {
        if(correct)
        {
            SpawnPhoneCall(GameManager.instance.currentCase.correctResult);
        } else 
        {
            SpawnPhoneCall(GameManager.instance.currentCase.incorrectResult);
        }
    }

    void SpawnPhoneCall(SimpleTextData textData)
    {
        DialogueLoader.instance.LoadMonologue(""+textData.order);
        DialogueLoader.instance.StartMonologue();
    }

    //void Spawn


}
