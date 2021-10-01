using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

//[CreateAssetMenu(fileName = "New SaveSystem Object", menuName = "Scriptable Objects/Save System")]
public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/Player.Data";


    public static void ClearSave()
    {
        File.Delete(path);
        Debug.Log("Delete Save Data");
    }

    public static void SavePlayer(Player player)
    {
        Debug.Log("Save");
        
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, player.playerData);
        Debug.Log("Max Jumps : " + player.playerData.maxJumps);
        stream.Close();
    }

    public static void LoadPlayer(Player player)
    {
        Debug.Log("Load");

        if (File.Exists(path))
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            player.playerData = (PlayerData)formatter.Deserialize(stream);
            Debug.Log("Max Jumps : " + player.playerData.maxJumps);
            stream.Close();
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
}
