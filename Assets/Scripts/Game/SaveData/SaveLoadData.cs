using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
public static class SaveLoadData
{
    public static void SaveData(GameManager game)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Game.cryptid";
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData(game);
        formatter.Serialize(stream, data);
        stream.Flush();
        stream.Close();
    }
    public static SaveData LoadData()
    {
        string path = Application.persistentDataPath + "/Game.cryptid";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        } else
        {
            Debug.LogError("Error: Save file not found in " + path);
            return null;
        }
    }
}