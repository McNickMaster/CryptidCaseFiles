using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicButtonData : MonoBehaviour
{

    public Interactable_Button button;
    public MyButtonType buttonType;
    private SimpleTextData myNewData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        GameEvents.instance.Event_SolveCase.AddListener(UpdateMyData);
        UpdateMyData();
    }
    void OnDisable()
    {
        GameEvents.instance.Event_SolveCase.RemoveListener(UpdateMyData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateMyData()
    {
        switch (buttonType)
        {
            case MyButtonType.Autopsy:
            {
                myNewData = GameManager.instance.currentCase.autopsyText;
                break;
            }

            case MyButtonType.Codex:
            {
                myNewData = GameManager.instance.currentCase.codexText;
                break;
            }
            case MyButtonType.Testimony:
            {
                myNewData = GameManager.instance.currentCase.testimonyText;
                break;
            }
        }


        button.textData = myNewData;
    }

}

public enum MyButtonType
{
    Autopsy, Codex, Testimony
}
