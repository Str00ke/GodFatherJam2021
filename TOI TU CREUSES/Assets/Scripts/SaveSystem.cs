using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    static string absPath = Application.dataPath + "/LevelsData";

    public static void Save(LevelData data)
    {

        int name = Directory.GetFiles(absPath, "*.data", SearchOption.TopDirectoryOnly).Length;
        BinaryFormatter formatter = new BinaryFormatter();

        string path = absPath + "/" + name + ".data";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, data);


        stream.Close();

        Debug.Log("Saved in: " + path);
    }

    public static LevelData Load()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);

        int fCount = Directory.GetFiles(absPath, "*.data", SearchOption.TopDirectoryOnly).Length - 1;
        int name = Random.Range(0, fCount);

        string path = absPath + "/" + name + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Error: Save file not found in: " + path);
            return null;
        }
    }
}
