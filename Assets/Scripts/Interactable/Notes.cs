using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Notes : Interactable
{

    public string[] textSlides;
    public TextMeshPro textBody;
    public LoadTextFromJson jsonText;

    private int currentTextIndex = 0;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Awake()
    {
        //SetBodyTextToIndex(currentTextIndex);

        rb = GetComponent<Rigidbody>();

        jsonText.LoadJson();
        textSlides = jsonText.GetNotePages();
        SetBodyTextToIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.AddForce(10*Vector3.forward);
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
