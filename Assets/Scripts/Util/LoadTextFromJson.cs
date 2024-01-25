using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class LoadTextFromJson : MonoBehaviour
{

    public string fileName = "";

    ListItem items;

    string[] text;

    // Start is called before the first frame update
    void Awake()
    {
        fileName = "Assets/TextSRC/" + gameObject.name + ".json";
        //LoadJson();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadJson()
    {
        StreamReader r = new StreamReader(fileName);
        string json = r.ReadToEnd();
        items = JsonUtility.FromJson<ListItem>(json);
        Debug.Log(items.Values.Length);
        text = new string[items.Values.Length];
        for(int i=0;i<items.Values.Length;i++)
        {
            text[i] = items.Values[i].Text;
            Debug.Log(items.Values[i].Text);
        }
    }

    public string[] GetNotePages()
    {
        return text;
    }
}

[Serializable]
public class ListItem {
    public Values[] Values;
}

[Serializable]
public class Values
{
    public string Text;
}