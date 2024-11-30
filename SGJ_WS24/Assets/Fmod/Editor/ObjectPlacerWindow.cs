using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine;
using UnityEngine;
using UnityEditor;

public class ObjectPlacerWindow : EditorWindow
{
    private GameObject beatPrefab;
    private GameObject markerPrefab;
    private Vector3 strartPosition;
    private Vector3 currPos = Vector3.zero;
    private float speed;
    private float time;
    private float curtime = 0;

    private bool shoudPlay;
    private bool shoudPlace;

    [MenuItem("Tools/Object Placer")]
    public static void ShowWindow()
    {
        GetWindow<ObjectPlacerWindow>("Object Placer");
    }

    private void OnGUI()
    {
        GUILayout.Label("Place Objects in Scene", EditorStyles.boldLabel);

        beatPrefab = (GameObject) EditorGUILayout.ObjectField("beatPrefab", beatPrefab, typeof(GameObject), false);
        markerPrefab = (GameObject) EditorGUILayout.ObjectField("makerPrefab", markerPrefab, typeof(GameObject), false);
        strartPosition = EditorGUILayout.Vector3Field("startPosition", strartPosition);

        speed = EditorGUILayout.FloatField("Speed", speed);
        time = EditorGUILayout.FloatField("Time", time);

        if (GUILayout.Button("Start"))
        {
            if (beatPrefab != null)
            {
               
            }
            else
            {
                Debug.LogWarning("No prefab assigned!");
            }
        }

        if (GUILayout.Button("Stop"))
        {
            Stop();
            BeatTrackerr.fixedBeatUpdate -= ShoudPlaceBeat;
        }
    }

    private void Startt()
    {
        try
        {
            currPos += new Vector3(0, Mathf.Round(curtime), 0);

            if (shoudPlace)
            {
                GameObject newObject = Instantiate(beatPrefab);
                newObject.transform.position = currPos;
                Undo.RegisterCreatedObjectUndo(newObject, "Place Object");
                EditorUtility.SetDirty(newObject);
                shoudPlace = false;
            }

            curtime += speed * Time.deltaTime;
            Debug.Log(curtime);
        }
        catch (Exception e)
        {
            Debug.Log("fuck that");
        }
    }

    private void Update()
    {
        if (shoudPlay)
            Startt();
    }

    private void Stop()
    {
        shoudPlay = false;
    }

    private void ShoudPlaceBeat()
    {
        shoudPlace = true;
    }
}