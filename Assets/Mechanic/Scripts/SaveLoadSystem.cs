using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; 
using UnityEngine;
using UnityEditor; 

[System.Serializable]

public class SaveLoadSystem
{
    public static SaveLoadSystem instance;
 
  //  public int LevelIndex = -1;
 
   // public int Supporter  { get { return _Supporter; } set { _Supporter = value; UIManager.instance.UpdateSupporterText(); } }
     //int _Supporter = 0;

    public int[] UpgradeIndex = {-1, -1, -1, -1, -1, -1, -1, -1, 0, 0, 0, 0};
    
    //Table
    public int[] TableUnlock = new[] {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1};
    private int tableSeatAmount = 1;

    public int TableSeatAmount
    {
        get => tableSeatAmount;
        set => tableSeatAmount = value;
    }

    public static void Save()
    {
        string path = Application.persistentDataPath + "/save.dat";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(path, FileMode.OpenOrCreate);
        bf.Serialize(file, instance);
        file.Close();
        
    }

    public static void Load()
    {
        
        string path = Application.persistentDataPath + "/save.dat";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            SaveLoadSystem data = (SaveLoadSystem)bf.Deserialize(file);

            file.Close();
            instance = data;

        }
        else
        {
            instance = new SaveLoadSystem();
          
            Save();

        }
    }
#if UNITY_EDITOR

    [MenuItem("SAVELoadSystem/Clear Save File")]
    private static void ClearSave()
    {
        string path = Application.persistentDataPath + "/save.dat";
        if (File.Exists(path))
        {
            File.Delete(path);

        }
    }
     
#endif   
    
}