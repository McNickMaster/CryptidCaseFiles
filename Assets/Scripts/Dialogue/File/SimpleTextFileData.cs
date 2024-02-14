using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SimpleTextFileData
{

    public string[] textPages;
    public SimpleTextFileData (string[] data)
    {

        textPages = data;
    }
    public string[] GetFilePages()
    {
        return textPages;
    }


}
