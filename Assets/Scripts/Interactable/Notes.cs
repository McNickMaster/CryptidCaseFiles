using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Notes : Interactable
{

    public string[] textSlides;
    public TextMeshPro textBody;
    //public LoadTextFromJson jsonText;
    public SimpleTextData textData;
    public GameObject[] pageButtons;
    public Button[] noteButtons;

    public Vector3 normal = Vector3.forward;

    private int currentTextIndex = 0;
    private Rigidbody rb;




    // Start is called before the first frame update
    void Awake()
    {
        //SetBodyTextToIndex(currentTextIndex);

        rb = GetComponent<Rigidbody>();

        

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.AddForce(25*normal);
    }

    public void Enable()
    {

    }

    public void Disable()
    {

    }

    public void Init()
    {
        SimpleTextFileData data = SaveLoadData.LoadText(""+textData.order);

        string[] temp = data.GetFilePages();
        textSlides = new string[temp.Length];
        for(int i = 0; i < data.GetFilePages().Length; i++)
        {
            string s = temp[i];
            s = s.Replace("|", System.Environment.NewLine); 
            textSlides[i] = s;
        }
        SetBodyTextToIndex(0);

        if(textSlides.Length < 2)
        {
            DisablePageButtons();
        }
    }

    public void SetBodyTextToIndex(int i)
    {
        currentTextIndex = i;
        textBody.text = textSlides[i];
    }

    public void PageTurn_Left()
    {
        if(currentTextIndex > 0)
        {
            currentTextIndex--;
            textBody.text = textSlides[currentTextIndex];
        }
    }
    public void PageTurn_Right()
    {
        if(currentTextIndex < textSlides.Length - 1)
        {
            currentTextIndex++;
            textBody.text = textSlides[currentTextIndex];
        }
    }

    public void DisablePageButtons()
    {
        pageButtons[0].SetActive(false);
        pageButtons[1].SetActive(false);
    }

    public void DisableNote()
    {
        //this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    public override void Interact()
    {
        //this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
