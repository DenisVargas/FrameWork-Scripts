using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Point))]
public class PointEditor : Editor
{
    public static GameObject Prefab;

    public override void OnInspectorGUI()
    {
        //Usa esto para acceder a las propiedades del script.
        Point currentPoint = (Point)target; //Referencia al script inspeccionado.

        Prefab = (GameObject)EditorGUILayout.ObjectField("El prefab", Prefab, typeof(GameObject), false);

        if (GUILayout.Button("Add new Conection"))
        {
            UnityEngine.MonoBehaviour.print("Worth");

            var GO_newPoint = Instantiate(Prefab, currentPoint.transform.position + currentPoint.transform.forward, Quaternion.identity);
            var newPoint = GO_newPoint.GetComponent<Point>();

            currentPoint.adjacents.Add(newPoint);
            newPoint.adjacents.Add(currentPoint);

            GO_newPoint.name = string.Format("Point({0})", ++Point.Instances);

            Selection.activeGameObject = GO_newPoint;
        }

        base.OnInspectorGUI(); //Todo lo normal.
    }
}
