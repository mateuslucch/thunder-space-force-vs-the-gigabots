using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    string filePath;

    private void Awake()
    {
        filePath = Application.persistentDataPath + "/save.score"; // where file will be saved       
    }

    public void SaveRecord(List<RecordData> saveScore)
    {
        FileStream dataStream = new FileStream(filePath, FileMode.Create);

        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(dataStream, saveScore);

        dataStream.Close();
    }

    public List<RecordData> LoadRecords()
    {
        if (File.Exists(filePath))
        {
            // File exists  
            FileStream dataStream = new FileStream(filePath, FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            List<RecordData> loadedData = converter.Deserialize(dataStream) as List<RecordData>;

            dataStream.Close();
            return loadedData;
        }
        else
        {
            // File does not exist
            Debug.Log($"<color=#00FF3C>Save file not found in {filePath}</color>");
            return null;
        }
    }
}
