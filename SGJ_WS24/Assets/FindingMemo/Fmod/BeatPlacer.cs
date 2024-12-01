using System;
//using UnityEditor;
using UnityEngine;

public class BeatPlacer : MonoBehaviour
{
   [SerializeField] private GameObject beatPrefab;
   [SerializeField]private GameObject markerPrefab;
   [SerializeField]private Vector3 strartPosition;
    private Vector3 currPos = Vector3.zero;
    [SerializeField] private float speed;
    //[SerializeField] private float time;
    private float curtime = 0;

    private bool shoudPlay;
    private bool shoudPlaceBeat;
    private bool shoudPlaceMarker;
    /*
    private void Startt()
    {
        try
        {
            curtime += speed * Time.deltaTime;
            currPos = new Vector3(0, curtime, 0);

            if (shoudPlaceBeat)
            {
                GameObject newObject = Instantiate(beatPrefab,transform);
                newObject.transform.position = currPos;
                Undo.RegisterCreatedObjectUndo(newObject, "Place Object");
                EditorUtility.SetDirty(newObject);
                shoudPlaceBeat = false;
            }
            
            if (shoudPlaceMarker)
            {
                GameObject newObject = Instantiate(markerPrefab,transform);
                newObject.transform.position = currPos;
                Undo.RegisterCreatedObjectUndo(newObject, "Place Object");
                EditorUtility.SetDirty(newObject);
                shoudPlaceMarker = false;
            }
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

    [ContextMenu(nameof(Play))]
    private void Play()
    {
        BeatTrackerr.instance.StartMusic();
        BeatTrackerr.fixedBeatUpdate -= ShoudPlaceBeat;
        BeatTrackerr.fixedBeatUpdate += ShoudPlaceBeat;
        
        BeatTrackerr.markerUpdated -= ShoudPlaceMarker;
        BeatTrackerr.markerUpdated += ShoudPlaceMarker;

        shoudPlay = true;
        curtime = 0;
    }
  
    [ContextMenu(nameof(Stop))]
    private void Stop()
    {
        shoudPlay = false;
        BeatTrackerr.fixedBeatUpdate -= ShoudPlaceBeat;
        BeatTrackerr.markerUpdated -= ShoudPlaceMarker;
    }
    
    private void ShoudPlaceBeat()
    {
        shoudPlaceBeat = true;
    }
    
    private void ShoudPlaceMarker()
    {
        shoudPlaceMarker = true;
    }
    */
}
