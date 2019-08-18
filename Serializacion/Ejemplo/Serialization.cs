using UnityEngine;
using System;
using Core.Serialization;

public class Serialization : MonoBehaviour
{
    public GameData myData;
    string path;
    void Start()
    {
        path = "Assets/Data/savedata.dat";
    }

    void Update()
    {

        //Binario.
        if (Input.GetKeyDown(KeyCode.F1))
        {
            print("Serializado papuh");
            FullSerialization.Serialize(myData, path);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            print("DeSerializado");
            myData = FullSerialization.Deserialize<GameData>(path);
        }

        //SERIALIZACION/DES EN JSON
        if (Input.GetKeyDown(KeyCode.F3))
        {
            print("JSon Serialize");
            FullSerialization.Serialize(myData, path, false);
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            print("DeSerializado JSon");
            myData = FullSerialization.Deserialize<GameData>(path, false);
        }

    }

    //void Serialize(bool binary)
    //{
    //    if (binary)
    //        BinarySerialize();
    //    else
    //        JSONSerialize();
    //}
    //void Deserialize(bool binary)
    //{
    //    if (binary)
    //        BinaryDeserialize();
    //    else
    //        JSONDeserialize();
    //}

    //private void JSONSerialize()
    //{
    //    StreamWriter file = File.CreateText(path);
    //    string json = JsonUtility.ToJson(myData, true);
    //    file.Write(json);
    //    file.Close();
    //}

    //private void JSONDeserialize()
    //{
    //    if (File.Exists(path))
    //    {
    //        string fileToLoad = File.ReadAllText(path);
    //        myData = JsonUtility.FromJson<GameData>(fileToLoad);
    //    }
    //}

    //void BinarySerialize()
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Create(path);

    //    bf.Serialize(file, myData);
    //    file.Close();
    //}

    //void BinaryDeserialize()
    //{
    //    if(File.Exists(path))
    //    {
    //        FileStream file = File.Open(path, FileMode.Open);
    //        BinaryFormatter bf = new BinaryFormatter();
    //        myData = (GameData)bf.Deserialize(file);
    //        file.Close();
    //    }
    //}
}

//Tarea:
//Funcion genérica para que le pueda pasar lo que se me cante.
//y que funcione


[Serializable]
public class GameData
{
    public string user;
    public string pasword;
    public int life;

    public GameData(string user, string pasword, int life)
    {
        this.user = user;
        this.pasword = pasword;
        this.life = life;
    }
}
