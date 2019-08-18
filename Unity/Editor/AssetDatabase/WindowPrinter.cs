using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class WindowPrinter : EditorWindow {
    /// <summary>
    /// 1.Chequear si un directorio Existe.
    /// 2.Chequear si un archivo Json Existe.
    ///     Si no, Hay que crear un archivo nuevo.
    /// 3.Cargar el archivo.
    /// 4.Mostrar el resultado.
    /// </summary>
    static int Index = 0;
    static int FolderIndex = 0;
    static string DataPath;
    static string JsonConfig;
    static string ConfigPath;


    [MenuItem("CustomWindow/JsonTest")]
	public static void OpenWindow()
    {
        var MainWindow = GetWindow<WindowPrinter>();
        MainWindow.Show();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Create the Object"))
        {
            CreateAClassInstance();
        }
        if (GUILayout.Button("Save the Object"))
        {
            SaveTheClassInstance();
        }
        if (GUILayout.Button("Load and Print"))
        {
            LoadTheClassInstance();
        }
    }

    private void LoadTheClassInstance()
    {
        string Data = File.ReadAllText(ConfigPath);
        ConfigSet LoadedData = JsonUtility.FromJson<ConfigSet>(Data);

        foreach (var Preset in LoadedData.Presets)
        {
            MonoBehaviour.print("Se ha cargado el preset ---> " + Preset.PresetName);
            foreach (var Folder in Preset.FolderConfigs)
            {
                MonoBehaviour.print("--> Se ha cargado la carpeta: " + Folder.Name);
            }
        }

        ProjectConfig.Configurations = LoadedData;
    }

    private void SaveTheClassInstance()
    {
        //Chequeo si existe la carpeta, sino, la creo.
        DataPath = Application.dataPath;
        MonoBehaviour.print(DataPath);
        if (!Directory.Exists(DataPath + "/Editor/Config"))
        {
            MonoBehaviour.print("La carpeta de configuracion no existe.\nSe ha creado una nueva carpeta de Configuracion en la ruta indicada.");
            AssetDatabase.CreateFolder("Assets/Editor", "Config");
        }
        else
            MonoBehaviour.print("La carpeta de configuracion existe.\nBuscando archivo de configuracion...");

        //Si existe Guardo el archivo.
        string JsonObject = JsonUtility.ToJson(ProjectConfig.Configurations);
        MonoBehaviour.print("String creado: " + JsonObject);
        string JsonFilePath = DataPath + "/Editor/Config/JsonSaveFile.json";
        ConfigPath = JsonFilePath;
        MonoBehaviour.print("Path creado: " + JsonFilePath);
        StreamWriter A = File.CreateText(JsonFilePath);
        A.Close();

        File.WriteAllText(JsonFilePath, JsonObject);
    }

    private void CreateAClassInstance()
    {
        MonoBehaviour.print("Cantidad de objetos al inicio: " + ProjectConfig.Configurations.Presets.Count);

        //Creo el nuevo preset.
        List<FolderConfig> FolderConfigs = new List<FolderConfig>() {
            new FolderConfig(FolderIndex++,string.Format("Carpeta {0}",FolderIndex) ,false,new float[]{ 1.5f,2.3f}),
            new FolderConfig(FolderIndex++,string.Format("Carpeta {0}",FolderIndex),true,new float[]{ 1f,2.3f}),
            new FolderConfig(FolderIndex++,string.Format("Carpeta {0}",FolderIndex),false,new float[]{ 1.5f,3.8f}),
            new FolderConfig(FolderIndex++,string.Format("Carpeta {0}",FolderIndex),true,new float[]{ 1.7f,2.6f}),
            new FolderConfig(FolderIndex++,string.Format("Carpeta {0}",FolderIndex),true,new float[]{ 1.5f,2.4f}),
            new FolderConfig(FolderIndex++,string.Format("Carpeta {0}",FolderIndex),false,new float[]{ 0.5f,3.63f})
        };//Configuraciones de carpeta.
        Preset PresetA = new Preset(Index++, true, string.Format("Preset {0}", Index), FolderConfigs);
        MonoBehaviour.print(string.Format("Se ha creado el objeto {0} y contiene {1} configuracion/es de carpeta.", Index, PresetA.FolderConfigs.Count > 0 ? PresetA.FolderConfigs.Count : 0));
        foreach (var FolderConfig in PresetA.FolderConfigs)
        {
            MonoBehaviour.print("Carpeta: " + FolderConfig.Name);
        }

        ProjectConfig.Configurations.Presets.Add(PresetA);
        if (ProjectConfig.Configurations.Presets.Count > 0)
        {
            foreach (var Preset in ProjectConfig.Configurations.Presets)
            {
                MonoBehaviour.print("Preset name: " + Preset.PresetName);
                foreach (var folder in Preset.FolderConfigs)
                {
                    MonoBehaviour.print("Folder: " + folder.Name);
                }
            }
        }
        else
        {
            MonoBehaviour.print("No se ha guardado ninguna configuracion");
        }
        
    }
}
