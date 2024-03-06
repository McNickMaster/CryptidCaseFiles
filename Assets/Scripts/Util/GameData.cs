using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData : MonoBehaviour
{
    public static GameData instance;

[Header("Prefab")]
    public GameObject interactableNamePrefab;

[Header("Polaroid Prefabs")]
    public GameObject[] CULPRIT_MUGSHOTS;

    public GameObject[] CAUSE_MUGSHOTS;
    public GameObject[] VICTIM_MUGSHOTS;

[Header("Audio")]
    public AudioClip pageTurn;
    public AudioClip puzzleDrop;
    public AudioClip evidenceFound;
[Header("Config")]
    public float PUZZLE_AUTO_LOCK = 0.3f;

[Header("Filepaths")]
    public string EDITOR_FILE_PATH = "Assets/TextSRC/";
    public string BUILD_FILE_PATH = "data/";

    void Awake()
    {
        instance = this;
    }







}

public enum e_Scene
{
    OFFICE, CS1, CS2, CS3, CS4, CS5
}
