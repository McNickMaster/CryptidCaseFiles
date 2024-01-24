using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextPage : MonoBehaviour
{
    public Button buttonNext, buttonFirst;
    public GameObject MythBookP1, MythBookP2;
    public Transform MythBook;

    private void Awake()
    {
        MythBookP1.SetActive(true);
        MythBookP2.SetActive(false);
    }

    void Start()
    {
        buttonNext.onClick.AddListener(TaskOnClickNext);
        buttonFirst.onClick.AddListener(TaskOnClickBack);
    }

    void TaskOnClickNext()
    {
        MythBookP2.SetActive(true);
    }
    void TaskOnClickBack()
    {
        MythBookP2.SetActive(false);
    }
}
