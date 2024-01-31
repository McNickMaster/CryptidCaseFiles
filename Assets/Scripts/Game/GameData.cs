using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData : MonoBehaviour
{
    public static GameData instance;

    //this is in order of the Culprit enum
    public GameObject[] CULPRIT_MUGSHOTS;

    //this is in order of the CauseOfDeath enum
    public GameObject[] CAUSE_MUGSHOTS;

    void Awake()
    {
        instance = this;
    }







}
