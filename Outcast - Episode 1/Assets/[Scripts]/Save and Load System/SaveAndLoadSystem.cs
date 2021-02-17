using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveAndLoadSystem
{
    static string path = Application.persistentDataPath + "/baroot.data";
   public static void SaveGame(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        GameDataBinary saveData = new GameDataBinary(data);

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public static GameDataBinary LoadGame()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameDataBinary data = formatter.Deserialize(stream) as GameDataBinary;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
