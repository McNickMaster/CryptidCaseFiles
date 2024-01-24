using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool menuOpen;

    private void Start()
    {
        pauseMenu.SetActive(false);
        menuOpen = false;
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            pauseMenu.SetActive(true);
            menuOpen = true;
        }
        else if (Input.GetKey("escape") && menuOpen)
        {
            pauseMenu.SetActive(false);
            menuOpen = false;
        }
    }
}
