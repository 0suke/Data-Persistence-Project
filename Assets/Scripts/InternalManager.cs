using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

public class InternalManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static InternalManager Instance;

    public int score;
    public int highestScore;
    public List<int> scoreList;
    public string playerName;
    public string highestName;
    public List<string> nameList;
    public int saveCount = 5;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void UpdateHighestInfo()
    {
        highestScore = scoreList[0];
        highestName = nameList[0];
    }

    public void CheckScore(int s)
    {
        for (int i = 0; i < scoreList.Count; i++)
        {
            if(s > scoreList[i])
            {
                scoreList.Insert(i, s);
                nameList.Insert(i, playerName);
                break;
            }
        }
        if (scoreList.Count > saveCount)
        {
            scoreList.RemoveAt(saveCount);
            nameList.RemoveAt(saveCount);
        }
        UpdateHighestInfo();
    }

    [System.Serializable]
    class SaveData
    {
        public List<int> scoreList;
        public List<string> nameList;
    }

    public void SaveGameJson()
    {
        SaveData data = new SaveData();
        data.scoreList = scoreList;
        data.nameList = nameList;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
    }

    public void LoadGameJson()
    {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            scoreList = data.scoreList;
            nameList = data.nameList;
            return;
        }
    }

    public void RemonveGameJson()
    {
        string path = Application.persistentDataPath + "savefile.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
        InitList();
        SaveGameJson();
    }

    public void InitList()
    {
        // ÉäÉXÉgÇãÛÇ…Ç∑ÇÈÅB
        scoreList = new List<int> {0};
        nameList = new List<string> {"Name"};
    }
}
