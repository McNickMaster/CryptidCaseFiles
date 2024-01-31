using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData : MonoBehaviour
{
    public static GameData instance;

    public GameObject[] CULPRIT_MUGSHOTS;

    public GameObject[] CAUSE_MUGSHOTS;

    void Awake()
    {
        instance = this;
    }







}
