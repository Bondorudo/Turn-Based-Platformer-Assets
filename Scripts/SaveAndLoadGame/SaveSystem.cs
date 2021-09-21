using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static bool doesSaveExist;

    public static void ClearSave()
    {
        string path = Application.persistentDataPath + "/gameSaveFiles.data";
        File.Delete(path);
        Debug.Log("Delete Save Data");
    }

    public static void SavePlayer(Player player)
    {
        doesSaveExist = true;

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameSaveFiles.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/gameSaveFiles.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
