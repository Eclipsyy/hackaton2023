using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class SaveSystem : MonoBehaviour
{
    /*
    public int score;
    public int highScore;
    public static SaveSystem ss;
    //public GameManager gameManager;

    private void Awake()
    {
        ss = GetComponent<SaveSystem>();
        LoadGame();
    }
    public void SaveGame()
    {
        if (score > highScore)
        {
            highScore = score;
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath
          + "/MySaveData.dat");
        SaveData data = new SaveData();
        data.highScore = highScore;
        bf.Serialize(file, data);
        file.Close();
        //Debug.Log("Game data saved!");
    }

    public void LoadGame()
    {

        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
              File.Open(Application.persistentDataPath
              + "/MySaveData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            highScore = data.highScore;
            //Debug.LogError("Loaded");
        }
        else
            SaveGame();
            //Debug.LogError("There is no save data!");
    }
    */
}
/*
[Serializable]
class SaveData
{
    public int highScore;
}
*/

