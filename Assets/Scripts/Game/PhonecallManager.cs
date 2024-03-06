using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PhonecallManager : MonoBehaviour
{
    [HideInInspector]
    public List<DialogueData> phoneCalls = new List<DialogueData>();
    public Interactable_Button phoneToggle;

    public GameObject[] buttons;

    void OnEnable()
    {
        SetPhoneCallList(GameManager.instance.currentCase.phoneCallList);
        PlayerInput.instance.phoneCallUIActive = true;
    }

    void OnDisable()
    {
        PlayerInput.instance.phoneCallUIActive = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPhoneCallList(List<DialogueData> newPhoneCalls)
    {
        phoneCalls = newPhoneCalls;

        if(phoneCalls.Count < 1)
        {
            phoneToggle.enabled = false;
        }

        DialogueData tempPhoneCall;
        Button tempButton;
        for(int i = 0; i < phoneCalls.Count; i++)
        {
            tempButton = buttons[i].GetComponent<Button>();
            tempPhoneCall = phoneCalls[i];
            tempButton.onClick.AddListener(delegate {DialogueLoader.instance.LoadConversation(tempPhoneCall.fileName);});
            //tempButton.onClick.AddListener(Ow);
            buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = tempPhoneCall.name;
        }
    }

    void Ow()
    {
        Debug.Log("owwwwwww");
    }

}
