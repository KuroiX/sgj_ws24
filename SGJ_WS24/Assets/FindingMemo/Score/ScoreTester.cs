using UnityEngine;

public class ScoreTester : MonoBehaviour
{
    private void Awake()
    {
        var scoreManager = GetComponent<Score>();
        
        //scoreManager.OnScoreChange += u => Debug.Log(u);
        //scoreManager.OnHit += u => Debug.Log(u);
    }
}