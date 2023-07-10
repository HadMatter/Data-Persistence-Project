using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataCenter : MonoBehaviour
{
    public static DataCenter Instance;
    public int highScore;
    public string highScoreHolder;


    private void Awake()
    {
        if (DataCenter.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        LoadGame();
        DontDestroyOnLoad(gameObject);
    }

    
    // ============= SAVE DATA ================

    [System.Serializable]
    class SaveData
    {
        public int highScore;
        public string highScoreHolder;
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        data.highScoreHolder = highScoreHolder;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText("D:\\Documents\\Unity\\Unity Projects\\Learning\\Junior Programmer Path\\Data-Persistence-Project\\SaveData\\savedata.json", json);
    }

    public void LoadGame()
    {
        string path = "D:\\Documents\\Unity\\Unity Projects\\Learning\\Junior Programmer Path\\Data-Persistence-Project\\SaveData\\savedata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScore = data.highScore;
            highScoreHolder = data.highScoreHolder;
        }
    }
}
