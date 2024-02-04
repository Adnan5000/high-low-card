using System.IO;
using UnityEngine;

namespace HighLow.Scripts.Common
{
    public static class DataSaver
    {
        public static void SaveData<T>(T data, string savePath)
        {
            string jsonData = JsonUtility.ToJson(data, true);
            File.WriteAllText(savePath, jsonData);
        }

        public static T LoadData<T>(string savePath)
        {
            if (File.Exists(savePath))
            {
                string jsonData = File.ReadAllText(savePath);
                return JsonUtility.FromJson<T>(jsonData);
            }
            else
            {
                Debug.LogWarning("Save file not found");
                return default(T);
            }
        }

        public static void DeleteData(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log("File deleted: " + filePath);
            }
            else
            {
                Debug.LogWarning("File not found: " + filePath);
            }
        }
    }
}
