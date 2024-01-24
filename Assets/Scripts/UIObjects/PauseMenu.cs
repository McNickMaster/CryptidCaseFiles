using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button exit, resume;

    private void Start()
    {
        exit.onClick.AddListener(Exit);
        resume.onClick.AddListener(Resume);
    }
    
    void Exit()
    {
        Application.Quit();
    }
    void Resume()
    {
        gameObject.SetActive(false);
    }
}
