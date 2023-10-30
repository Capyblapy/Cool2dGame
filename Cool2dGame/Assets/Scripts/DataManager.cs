using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public SaveData LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/not.data";
        //print(filePath);
        if (File.Exists(filePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(filePath, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            print("Loaded data from " + filePath);

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("Save file not found in " + filePath + ", creating new save data");

            //Money = Money;

            SaveData data = new SaveData(gameObject.GetComponent<InventoryManager>());
            SaveGameData();

            return data;
        }
    }

    public void SaveGameData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/not.data";
        FileStream stream = new FileStream(filePath, FileMode.Create);

        SaveData data = new SaveData(gameObject.GetComponent<InventoryManager>());
        print("Saved data at " + filePath);

        formatter.Serialize(stream, data);
        stream.Close();
    }
}

[System.Serializable]
public class SaveData
{
    public List<GameObject> Inventory;

    public SaveData(InventoryManager im)
    {
        Inventory = im.Inventory;
    }
}