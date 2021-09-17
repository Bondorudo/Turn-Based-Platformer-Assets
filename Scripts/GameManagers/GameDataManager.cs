using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;

    GameData gameData;

    string saveFile;

    FileStream dataStream;

    BinaryFormatter converter = new BinaryFormatter();


    private void Awake()
    {
        instance = this;
        saveFile = Application.persistentDataPath + "/gamedata.data";
    }
    
    // Load Game
    /// <summary>
    /// Load Game Data
    /// </summary>
    public void ReadFile()
    {
        Debug.Log("READ FILE");
        if (File.Exists(saveFile))
        {
            Debug.Log("File Exists");
            dataStream = new FileStream(saveFile, FileMode.Open);

            gameData = converter.Deserialize(dataStream) as GameData;

            dataStream.Close();
        }
        else
        {
            Debug.Log("FILE DOES NOT EXIST");
            dataStream = new FileStream(saveFile, FileMode.Create);
        }
    }

    // Save Game
    /// <summary>
    /// Save Game Data
    /// </summary>
    public void WriteFile()
    {
        Debug.Log("WRITE FILE");
        dataStream = new FileStream(saveFile, FileMode.Create);

        converter.Serialize(dataStream, gameData);

        dataStream.Close();
    }
}


