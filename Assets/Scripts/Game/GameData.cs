using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData : MonoBehaviour
{
    public static GameData instance;

    public GameObject[] CULPRIT_MUGSHOTS;

    public GameObject[] CAUSE_MUGSHOTS;

    public const string EDITOR_FILE_PATH = "Assets/TextSRC/";
    public const string BUILD_FILE_PATH = "data/";

    void Awake()
    {
        instance = this;
    }







}
