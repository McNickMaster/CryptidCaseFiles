using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CSButtonScript : MonoBehaviour
{
    public Button clue1, clue2, clue3, clue4, clue5, firstResponder, k9, reporter, toDesk;
    public GameObject ClueBit1, ClueBit2, ClueBit3, ClueBit4, ClueBit5, FirstResponderBit, K9Bit, ReporterBit;
    public Transform Bits, Dialogue;

    private bool clue1Open, clue2Open, clue3Open, clue4Open, clue5Open, usedResource;

    private void Awake()
    {
        clue1Open = false;
        clue2Open = false;
        clue3Open = false;
        clue4Open = false;
        clue5Open = false;
        usedResource = false;
    }

    void Start()
    {
        clue1.onClick.AddListener(ClickedClue1);
        clue2.onClick.AddListener(ClickedClue2);
        clue3.onClick.AddListener(ClickedClue3);
        clue4.onClick.AddListener(ClickedClue4);
        clue5.onClick.AddListener(ClickedClue5);
        firstResponder.onClick.AddListener(ClickedResponder);
        k9.onClick.AddListener(ClickedK9);
        reporter.onClick.AddListener(ClickedReporter);
        toDesk.onClick.AddListener(ClickedDesk);
    }

    void ClickedDesk()
    {
        SceneManager.LoadScene("desk");
    }

    void ClickedClue1()
    {
        if (!clue1Open)
        {
            Instantiate(ClueBit1, transform.position, transform.rotation, Bits);
            clue1Open = true;
        }
    }
    void ClickedClue2()
    {
        if (!clue2Open)
        {
            Instantiate(ClueBit2, transform.position, transform.rotation, Bits);
            clue2Open = true;
        }
    }
    void ClickedClue3()
    {
        if (!clue3Open)
        {
            Instantiate(ClueBit3, transform.position, transform.rotation, Bits);
            clue3Open = true;
        }
    }
    void ClickedClue4()
    {
        if (!clue4Open)
        {
            Instantiate(ClueBit4, transform.position, transform.rotation, Bits);
            clue4Open = true;
        }
    }
    void ClickedClue5()
    {
        if (!clue5Open)
        {
            Instantiate(ClueBit5, transform.position, transform.rotation, Bits);
            clue5Open = true;
        }
    }
    void ClickedResponder()
    {
        if (!usedResource)
        {
            Instantiate(FirstResponderBit, Dialogue.transform.position, transform.rotation, Dialogue);
            usedResource = true;
        }
    }
    void ClickedK9()
    {
        if (!usedResource)
        {
            Instantiate(K9Bit, Dialogue.transform.position, transform.rotation, Dialogue);
            usedResource = true;
        }
    }
    void ClickedReporter()
    {
        if (!usedResource)
        {
            Instantiate(ReporterBit, Dialogue.transform.position, transform.rotation, Dialogue);
            usedResource = true;
        }
    }
}
