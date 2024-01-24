using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button buttonMyths, buttonTestimonies, buttonAutopsy, buttonCaseFile, buttonMap, buttonTack, buttonMug, buttonPhone, buttonLamp;
    public GameObject MythBook, Testimonies, Autopsy, CaseFile, Map, Tack;
    public GameObject CaseFileButton, CoffeeSip, NoMoreCoffee, PhoneDial, TurnOffLight;
    public Transform Documents, Desk, TackParent, CoffeeParent, PhoneParent, LightParent;

    private bool mythBookOpen, testimoniesOpen, autopsyOpen, caseFileOpen, mapOpen, phonePressed, lampClicked;
    private int sipsNum;

    private void Awake()
    {
        mythBookOpen = false;
        testimoniesOpen = false;
        autopsyOpen = false;
        mapOpen = false;
        caseFileOpen = false;
        phonePressed = false;
        lampClicked = false;
        sipsNum = 0;
    }

    void Start()
    {
        buttonMyths.onClick.AddListener(TaskOnClickMyth);
        buttonTestimonies.onClick.AddListener(TaskOnClickTest);
        buttonAutopsy.onClick.AddListener(TaskOnClickAut);
        buttonCaseFile.onClick.AddListener(TaskOnClickCase);
        buttonMap.onClick.AddListener(TaskOnClickMap);
        buttonTack.onClick.AddListener(TaskOnClickTack);
        buttonMug.onClick.AddListener(TaskOnClickMug);
        buttonPhone.onClick.AddListener(TaskOnClickPhone);
        buttonLamp.onClick.AddListener(TaskOnClickLamp);
    }

    private void Update()
    {
        if (!GameObject.FindWithTag("Codex"))
        {
            mythBookOpen = false;
        }
        if (!GameObject.FindWithTag("Testimonies"))
        {
            testimoniesOpen = false;
        }
        if (!GameObject.FindWithTag("Autopsy"))
        {
            autopsyOpen = false;
        }
        if (!GameObject.FindWithTag("Map"))
        {
            mapOpen = false;
        }
        if (!GameObject.FindWithTag("CaseFile"))
        {
            caseFileOpen = false;
            CaseFileButton.SetActive(true);
        }
        if (!GameObject.FindWithTag("Dialed"))
        {
            phonePressed = false;
        }
        if (!GameObject.FindWithTag("NextDay"))
        {
            lampClicked = false;
        }
    }

    void TaskOnClickMyth()
    {
        Debug.Log("Myth button clicked");
        if (!mythBookOpen)
        {
            Instantiate(MythBook, transform.position, transform.rotation, Documents);
            mythBookOpen = true;
        }
    }

    void TaskOnClickTest()
    {
        Debug.Log("Testimonies button clicked");
        if (!testimoniesOpen)
        {
            Instantiate(Testimonies, transform.position, transform.rotation, Documents);
            testimoniesOpen = true;
        }
    }

    void TaskOnClickAut()
    {
        Debug.Log("Autopsy button clicked");
        if (!autopsyOpen)
        {
            Instantiate(Autopsy, transform.position, transform.rotation, Documents);
            autopsyOpen = true;
        }
    }
    void TaskOnClickMap()
    {
        Debug.Log("Map button clicked");
        if (!mapOpen)
        {
            Instantiate(Map, transform.position, transform.rotation, Documents);
            mapOpen = true;
        }
    }
    void TaskOnClickCase()
    {
        Debug.Log("Case File button clicked");
        if (!caseFileOpen)
        {
            Instantiate(CaseFile, Desk.transform.position,/* + new Vector3(-620, -370, 0)*/ Desk.transform.rotation, Desk);
            caseFileOpen = true;
            CaseFileButton.SetActive(false);
        }
    }
    void TaskOnClickTack()
    {
        Instantiate(Tack, Input.mousePosition, transform.rotation, TackParent);
    }
    void TaskOnClickMug()
    {
        if (sipsNum >= 5)
        {
            Instantiate(NoMoreCoffee, CoffeeParent.transform.position, transform.rotation, CoffeeParent);
        }
        else if (sipsNum < 5)
        {
            Instantiate(CoffeeSip, CoffeeParent.transform.position, transform.rotation, CoffeeParent);
            sipsNum += 1;
        }
    }
    void TaskOnClickPhone()
    {
        if (!phonePressed)
        {
            Instantiate(PhoneDial, PhoneParent.transform.position, transform.rotation, PhoneParent);
            phonePressed = true;
        }
    }
    void TaskOnClickLamp()
    {
        if (!lampClicked)
        {
            Instantiate(TurnOffLight, LightParent.transform.position, transform.rotation, LightParent);
            lampClicked = true;
        }
    }
}
