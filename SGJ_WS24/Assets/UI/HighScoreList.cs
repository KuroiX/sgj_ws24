using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreList : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private HighScoreElementUI listElementPref;
    
    [ContextMenu(nameof(UpdateList))]
    public void UpdateList()
    {
        foreach (Transform child in container.transform)
        {
            Destroy(child.gameObject); // Destroy the child GameObject
        }

        foreach (var scoreObj in HightScoreManager.instance.scoreObjs)
        {
           var newObj=Instantiate(listElementPref, container.transform);
           newObj.gameObject.SetActive(true);
           newObj.Init(scoreObj.Name,scoreObj.Score.ToString());
        }
    }
}

