using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(Weapon))]
public class Editor_WeaponType : Editor
{
    //Cosas a utilizar CTM
    Weapon Editing;

    //public override void OnInspectorGUI()
    //{
    //    Editing = (Weapon)target;

    //    Attack entryPoint_light = null;

    //    EditorGUILayout.LabelField("Entry Points");

    //    EditorGUILayout.BeginHorizontal();
    //    Editing.entryPoints[Inputs.light] = (Attack)EditorGUILayout.ObjectField("Light Attack", Editing.entryPoints[Inputs.light], typeof(Attack), true);

    //    EditorGUI.BeginDisabledGroup(Editing.entryPoints == null);
    //    if (GUILayout.Button("Create Light", GUILayout.MaxWidth(100f)))
    //    {
    //        entryPoint_light = CreateInstance<Attack>();
    //        string desiredDataPath = Application.dataPath + "/Scriptable Objects/Basic_Light.asset";

    //        if (!AssetDatabase.IsValidFolder("Assets/Scriptable Objects"))
    //            AssetDatabase.CreateFolder("Assets","Scriptable Objects");
    //        AssetDatabase.CreateAsset(entryPoint_light, "Assets/Scriptable Objects/Basic_Light.asset");
    //        Editing.entryPoints[Inputs.light] = AssetDatabase.LoadAssetAtPath<Attack>(desiredDataPath);

    //        Selection.SetActiveObjectWithContext(Editing.entryPoints[Inputs.light], Selection.activeContext);

    //        MonoBehaviour.print("Añadiste una nueva arma!!");
    //    }
    //    EditorGUI.EndDisabledGroup();
    //    EditorGUILayout.EndHorizontal();
    //}
}
