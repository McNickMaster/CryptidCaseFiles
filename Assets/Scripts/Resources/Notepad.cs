using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notepad : MonoBehaviour
{
    public static Notepad Instance;
    public Button open, close;
    public GameObject notepad, openButton, showNotepad; 
    private GameObject noNotes;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        open.onClick.AddListener(Open);
        close.onClick.AddListener(Close);
    }
    void Open()
    {
        notepad.SetActive(true);
        openButton.SetActive(false);
    }
    void Close()
    {
        notepad.SetActive(false);
        openButton.SetActive(true);
    }
    private void Update()
    {
        if (noNotes = GameObject.FindWithTag("NoNotes"))
        {
            showNotepad.SetActive(false);
        }
        else
        {
            showNotepad.SetActive(true);
        }
    }
}
