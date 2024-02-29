using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public static class SaveLoadData
{
    public static void SaveData(GameManager game)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Save01.cryptid";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(game);
        formatter.Serialize(stream, data);
        Debug.Log(Application.persistentDataPath);
        stream.Flush();
        stream.Close();
    }
    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/Save01.cryptid";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        } else
        {
            //Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }

    public static void SaveDialogueData(List<Branch> branches, List<Slide> slides, int id)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = "Data/Dialogue"+id+".cryptid";
        FileStream stream = new FileStream(path, FileMode.Create);
        DialogueFileData data = new DialogueFileData(branches, slides);
        formatter.Serialize(stream, data);
        stream.Flush();
        stream.Close();
    }

    public static DialogueFileData LoadDialogue(int id)
    {
        string path = "Data/Dialogue"+id+".cryptid";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DialogueFileData data = formatter.Deserialize(stream) as DialogueFileData;
            stream.Close();
            return data;
        } else
        {
            //Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }


    public static void SaveSimpleTextData(string[] text, string id)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = "Data/Text"+id+".cryptid";
        FileStream stream = new FileStream(path, FileMode.Create);
        SimpleTextFileData data = new SimpleTextFileData(text);
        formatter.Serialize(stream, data);
        stream.Flush();
        stream.Close();
    }

    public static SimpleTextFileData LoadText(string id)
    {
        string path = "Data/Text"+id+".cryptid";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SimpleTextFileData data = formatter.Deserialize(stream) as SimpleTextFileData;
            stream.Close();
            return data;
        } else
        {
            Debug.LogError("Error: Text data file not found in " + path);
            return null;
        }
    }





}