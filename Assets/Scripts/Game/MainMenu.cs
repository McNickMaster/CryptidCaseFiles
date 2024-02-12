using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button start, settings, exit, closeSettings;
    public GameObject SettingsBit;

    void Start()
    {
        start.onClick.AddListener(StartGame);
        settings.onClick.AddListener(Settings);
        exit.onClick.AddListener(ExitGame);
        SettingsBit.SetActive(false);
        closeSettings.onClick.AddListener(GoBack);
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    void Settings()
    {
        SettingsBit.SetActive(true);
    }
    void ExitGame()
    {
        Application.Quit();
    }
    void GoBack()
    {
        SettingsBit.SetActive(false);
    }
}
