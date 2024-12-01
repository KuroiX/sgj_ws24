using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreElementUI : MonoBehaviour
{
    [SerializeField] public TMP_Text Name;
    [SerializeField] public TMP_Text Score;

    public void Init(string Name, string Score)
    {
        this.Name.text = Name;
        this.Score.text = Score;
    }
}
