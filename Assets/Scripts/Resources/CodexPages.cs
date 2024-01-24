using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodexPages : MonoBehaviour
{
    [TextArea(10, 20)]
    [SerializeField] private string content;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text text;

    public Button buttonNext, buttonPrevious/*, buttonAddPage*/;

    private void Awake()
    {
        buttonNext.onClick.AddListener(NextPage);
        buttonPrevious.onClick.AddListener(PreviousPage);
        //buttonAddPage.onClick.AddListener(AddInfo);
    }

    public void PreviousPage()
    {
        if (title.pageToDisplay < 1)
        {
            title.pageToDisplay = 1;
            return;
        }
        if (title.pageToDisplay -1 > 1)
        {
            title.pageToDisplay -=1;
        }
        else
        {
            title.pageToDisplay = 1;
        }
        text.pageToDisplay = title.pageToDisplay;
    }

    public void NextPage()
    {
        if (title.pageToDisplay >= title.textInfo.pageCount)
            return;
        else
        {
            title.pageToDisplay += 1;
            text.pageToDisplay += 1;
        }
    }

    /*public void AddInfo()
    {
        title.text += "<br>" +
            "More Info";
        text.text += "I wonder how this works and how to create extra lines" +
            "like this maybe" +
            "text" +
            "exttetsts";
    }*/
}
