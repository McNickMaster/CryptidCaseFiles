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

    void Awake()
    {
        
    }

    void OnEnable()
    {
        

        SetPhoneCallList(GameManager.instance.currentCase.phoneCallList);


        PlayerInput.instance.phoneCallUIActive = GameManager.instance.currentCase.phoneCallList.Count > 0;
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

        

        DialogueData tempPhoneCall;
        Button tempButton;
        for(int i = 0; i < phoneCalls.Count; i++)
        {
            tempButton = buttons[i].GetComponent<Button>();
            tempPhoneCall = phoneCalls[i];
            Debug.Log(tempPhoneCall.name + "  " + tempPhoneCall.fileName);
            tempButton.GetComponent<PhonecallData>().id = tempPhoneCall.fileName;
            //tempButton.onClick.AddListener(() => DialogueLoader.instance.LoadConversation(tempPhoneCall.fileName));
            string s = tempPhoneCall.name;
            s = s.Substring(6, s.Length - 6);
            buttons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = s;
        }
    }


}
