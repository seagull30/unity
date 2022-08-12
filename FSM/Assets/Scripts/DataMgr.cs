using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class GameData
{
    public int BGMVolume = 0;
    public int EffectVolume = 0;

    public int gold = 0;
    public int hp = 10;
    public float moveSpeed = 5f;

    public List<MonsterData> monsterKillDatas;

    public GameData(int _gold, int _hp, float _movesSpeed)
    {
        gold = _gold;
        hp = _hp;
        moveSpeed = _movesSpeed;
        monsterKillDatas = new List<MonsterData>();
    }
}

[Serializable]
public class MonsterData
{
    public int Index;
    public string Name;
    public float MoveSpeed;
    public float RotationSpeed;
    public string Description;

    public MonsterData(int index, string name, float moveSpeed, float rotationSpeed, string description)
    {
        this.Index = index;
        this.Name = name;
        this.MoveSpeed = moveSpeed;
        this.RotationSpeed = rotationSpeed;
        this.Description = description;
    }
}

public class DataMgr : MonoBehaviour
{
    static GameObject container;
    static GameObject Container { get => container; }

    static DataMgr instance;
    public static DataMgr Instance
    {
        get
        {
            if (instance == null)
            {
                container = new GameObject();
                container.name = "DataMgr";
                instance = Container.AddComponent(typeof(DataMgr)) as DataMgr;

                instance.SetMonsterDataFromCSV();

                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }

    GameData gameDatas;
    public GameData GameData
    {
        get
        {
            if (gameDatas == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return gameDatas;
        }

    }

    void InitGameData()
    {
        gameDatas = new GameData(100, 300, 5f);

        gameDatas.monsterKillDatas.Add(new MonsterData(1, "ÀüÁöÀ±", 2f, 1f, "¿À´Ã ¾È¿È"));
        gameDatas.monsterKillDatas.Add(new MonsterData(1, "±ÇÈñ¿µ", 2f, 1f, "¿À´Ã ¾È¿È"));
    }

    public void SaveGameData()
    {        
        string toJsonData = JsonUtility.ToJson(gameDatas, true);
        string filePath = Application.persistentDataPath + GameDataFileName;
        File.WriteAllText(filePath, toJsonData);
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;
        if(File.Exists(filePath))
        {
            string fromJsonData = File.ReadAllText(filePath);
            gameDatas = JsonUtility.FromJson<GameData>(fromJsonData);
            if(gameDatas ==null)
            {
                InitGameData();
            }

        }
        else
        {
            InitGameData();
        }
       
    }

    public string GameDataFileName = ".json";

    [Header("¸ó½ºÅÍ °ü·Ã DB")]
    [SerializeField] TextAsset monsterDB;

    public Dictionary<int, MonsterData> MonsterDataDict { get; set; }

    private void SetMonsterDataFromCSV()
    {
        monsterDB = Resources.Load<TextAsset>("CSV/GameData - Monster");
        if (monsterDB == null)
        {
            Debug.LogError("CSV/GameData - Monster ÆÄÀÏÀÌ ¾ø½À´Ï´Ù.");
            return;
        }

        if (MonsterDataDict == null)
        {
            MonsterDataDict = new Dictionary<int, MonsterData>();
        }


        string[] lines = monsterDB.text.Substring(0, monsterDB.text.Length).Split('\n');
        for (int i = 1; i < lines.Length; ++i)
        {
            string[] row = lines[i].Split(',');
            MonsterDataDict.Add(int.Parse(row[0]), new MonsterData(
                int.Parse(row[0]),  // index
                row[1],             // name 
                float.Parse(row[2]),// movespeed
                float.Parse(row[3]),// rotationspeed
                row[4]              // descripion
                ));
        }
    }

    public MonsterData GetMonsterDataFromCSV(int index)
    {
        if (MonsterDataDict.ContainsKey(index))
        {
            return MonsterDataDict[index];
        }
        return null;
    }
}
