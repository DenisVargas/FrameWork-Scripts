using System;
using System.Collections.Generic;

[System.Serializable]
public class FolderConfig{
    public int id;
    public string Name;
    public bool HasMultipleExtentions;
    public float[] numbers;


    public FolderConfig(int id, string Name, bool HasMultipleExtentions,float [] numbers)
    {
        this.id = id;
        this.Name = Name;
        this.HasMultipleExtentions = HasMultipleExtentions;
        this.numbers = numbers;
    }
}


[System.Serializable]
public class Preset
{
    public int PrefabIndex;
    public bool IsDefaultPreset;
    public string PresetName;
    public List<FolderConfig> FolderConfigs;

    public Preset()
    {

    }
    public Preset(int prefabIndex, bool IsDefaultPreset, string PresetName, List<FolderConfig> FolderConfigs)
    {
        this.PrefabIndex = prefabIndex;
        this.IsDefaultPreset = IsDefaultPreset;
        this.PresetName = PresetName;
        this.FolderConfigs = FolderConfigs;
    }
}

[System.Serializable]
public class ConfigSet
{
    public List<Preset> Presets = new List<Preset>();
}

//Clase que alberga las configuraciones.
public static class ProjectConfig
{
    public static ConfigSet Configurations = new ConfigSet();
}

