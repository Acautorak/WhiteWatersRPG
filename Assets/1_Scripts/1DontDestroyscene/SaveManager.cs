using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

// kad si vec narpavio i preko prefsa i binarno, bolje bi bilo da obe varijante imaju isti interfejs
// pa da mozes da ih koristis interchangeably, kao razlicite strategije za save sistem
public static class SaveManager
{
    public static void SaveJson(string key, object data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public static void SaveGoldGems(int goldValue, int gemsValue)
    {
        PlayerPrefs.SetInt("gold", goldValue);
        PlayerPrefs.SetInt("gems", gemsValue);
        PlayerPrefs.Save();
    }

    public static void LoadGoldGems()
    {
        int gold = PlayerPrefs.GetInt("gold", 2000);
        int gems = PlayerPrefs.GetInt("gems", 10);
        PartyManager.Instance.gold = gold;
        PartyManager.Instance.gems = gems;
    }

    public static T LoadJson<T>(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(json);
        }
        else
        {
            Debug.LogWarning($"Key {key} not found. Returning default value.");
            return default(T);
        }
    }

    public static void SaveListJson<T>(string key, List<T> dataList)
    {
        string json = JsonUtility.ToJson(new Serialization<T>(dataList));
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public static List<T> LoadListJson<T>(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            Serialization<T> serialization = JsonUtility.FromJson<Serialization<T>>(json);
            return serialization != null ? serialization.ToObject() : new List<T>();
        }
        else
        {
            Debug.LogWarning($"Key {key} not found. Returning empty list.");
            return new List<T>();
        }
    }

    [Serializable]
    private class Serialization<T>
    {
        public List<T> items;

        public Serialization(List<T> data)
        {
            items = data;
        }

        public List<T> ToObject()
        {
            return items;
        }
    }

    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if(!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "/saves" + saveName + ".save";

        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);

        file.Close();

        return true;
    }

    public static object Load(string path)
    {
        if(!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load at {0}, path");
            file.Close();
            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        return binaryFormatter;
    }
}
