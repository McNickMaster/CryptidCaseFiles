using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject currentLocation;
    public View currentView;
    public View[] views;
    public GameObject lineDrawer;
    public CaseFile caseFileObj;

    public Case[] cases;
    private Case currentCase;
    private Case currentGuess;
    private int caseIndex;

    void Awake()
    {
        instance = this;

        currentCase = cases[0];
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
            TrySolveCase();
        }
    }

    public void TrySolveCase()
    {
        currentGuess = caseFileObj.GetGuess();


       if(currentCase.SolveCase(currentGuess))
       {
            NextCase();
            Debug.Log("Case solved");
       } else 
       {
            Debug.Log("Case NOT solved you bozo");
       }
    }

    void SetNewCase(Case newCase)
    {
        currentCase = newCase;
    }

    void NextCase()
    {
        caseIndex++;
        SetNewCase(cases[caseIndex]);
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


[Serializable]
public class SerializableEnum<T> where T : struct, IConvertible
{
    public T Value
    {
        get { return m_EnumValue; }
        set { m_EnumValue = value; }
    }
    private string m_EnumValueAsString;
    [SerializeField]
    private T m_EnumValue;
}