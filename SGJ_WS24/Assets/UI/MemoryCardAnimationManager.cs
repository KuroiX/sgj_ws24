using System;
using DG.Tweening;
using UnityEngine;

public class MemoryCardAnimationManager : MonoBehaviour
{
    [SerializeField] private DOTweenPath dotPathIn;
    [SerializeField] private Ease inEase;
    [SerializeField] private float inDuration;
    
    [SerializeField] private DOTweenPath dotPathOut;
    [SerializeField] private Ease outEase;
    [SerializeField] private float outDuration;
    
    [SerializeField] private GameObject TestCard;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayInAnimation(TestCard);
        } 
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayOutAnimationYeet(TestCard);
        } 
        
        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayOutAnimationNormal(TestCard);
        } 
    }

    public void PlayInAnimation(GameObject memoryCard)
    {
        memoryCard.transform.DOPath(dotPathIn.path, 2f).SetAutoKill(false);
        memoryCard.transform.DORotate(new Vector3(0,0,360), 2f,RotateMode.FastBeyond360).SetEase(inEase);
    }
    
    
    public async void PlayOutAnimationYeet(GameObject memoryCard)
    {
        try
        {
            await memoryCard.transform.DOPunchScale(new Vector3(0.6f,0.6f,0.6f), 0.8f, 5,0.5f).SetEase(Ease.OutQuad).AsyncWaitForCompletion();
            memoryCard.transform.DOPath(dotPathOut.path, 1f).SetAutoKill(false);
            await memoryCard.transform.DORotate(new Vector3(0,0,360), 0.3f,RotateMode.FastBeyond360).SetEase(inEase).SetLoops(5, LoopType.Incremental).AsyncWaitForCompletion();
            Destroy(memoryCard.gameObject);
        }
        catch (Exception e)
        {
            throw; // TODO handle exception
        }
    }
    
    public async void PlayOutAnimationNormal(GameObject memoryCard)
    {
        try
        {
            memoryCard.transform.DOScale(new Vector3(1.5f,1.5f,1.5f), 1f).SetAutoKill(true);
            await memoryCard.GetComponent<Renderer>().material.DOFade(0, 1f).AsyncWaitForCompletion();
            Destroy(memoryCard.gameObject);
        }
        catch (Exception e)
        {
            throw; // TODO handle exception
        }
    }
}



