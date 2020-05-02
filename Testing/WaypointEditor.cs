using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint ins;

    List<Vector3> targetpositions = new List<Vector3>();

    private void OnEnable()
    {
        ins = target as Waypoint;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Add point"))
        {
            ins.AddPoint(Vector3.zero, Quaternion.identity);
            ins.GetPointsInWorld();

            var Childs = ins.transform.GetComponentsInChildren<Transform>();
            for (int i = 1; i < Childs.Length; i++)
            {
                var child = Childs[i];
                child.gameObject.name = string.Format("point Position ({0})", i);
            }

            Debug.Log("Testing Testing");
        }

        if (GUILayout.Button("Clear Data"))
        {
            ins.point.Clear();
        }

    }

    public void OnSceneGUI()
    {
        EditorGUI.BeginChangeCheck();
        if (ins.point != null)
        {

            targetpositions = new List<Vector3>(ins.point);

            for (int i = 0; i < ins.point.Count; i++)
            {
                targetpositions[i] = Handles.FreeMoveHandle(targetpositions[i], Quaternion.identity, 1, Vector3.one, Handles.CircleHandleCap);
            }
        }

        if (EditorGUI.EndChangeCheck())
        {

        }
    }
}
