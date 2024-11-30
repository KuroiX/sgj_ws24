using System;
using UnityEngine;

public class MemoryCardManager : MonoBehaviour
{
    [SerializeField] private MemoryCardAnimationManager memoryCardAnimationManager;
    [SerializeField] private MemoryCard memoryCardPrefab;
    [SerializeField] private MemoryCardInfo[] memoryCardObjs;
    
    [SerializeField] private int currentMemoryCardIndex = 0;
    [SerializeField] private MemoryCard currentMemoryCard;
    
    [ContextMenu(nameof(SpawnNextMemoryCard))]
    public void SpawnNextMemoryCard(MemoryCardInfo memoryCardInfo)
    {
        
        currentMemoryCard = Instantiate(memoryCardPrefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
        currentMemoryCard.Init(memoryCardInfo.sprite);
        
        memoryCardAnimationManager.PlayInAnimation(currentMemoryCard.gameObject);

        memoryCardInfo.isSpawned = true;
    }
    
    [ContextMenu(nameof(YeetMemoryCard))]
    public void YeetMemoryCard()
    {
        memoryCardAnimationManager.PlayOutAnimationYeet(currentMemoryCard.gameObject);
    }
    
    [ContextMenu(nameof(OutMemoryCard))]
    public void OutMemoryCard()
    {
        memoryCardAnimationManager.PlayOutAnimationNormal(currentMemoryCard.gameObject);
    }

    public uint testScore;

    [ContextMenu(nameof(UpdateScore))]
    public void UpdateScore()
    {
        OnScoreChanged(testScore);
    }

    public void OnScoreChanged(uint newScore)
    {
        
        var newMemoryCardInfo = memoryCardObjs[currentMemoryCardIndex];

        if (newMemoryCardInfo.scoreIn <= newScore && !newMemoryCardInfo.isSpawned)
        {
            SpawnNextMemoryCard(newMemoryCardInfo);
            return;
        }
        
        if (newMemoryCardInfo.scoreOut <= newScore && newMemoryCardInfo.isSpawned)
        {
            switch (newMemoryCardInfo.outAnimation)
            {
                case OutAnimation.Normal:
                    memoryCardAnimationManager.PlayOutAnimationNormal(currentMemoryCard.gameObject);
                    currentMemoryCardIndex++;
                    break;
                case OutAnimation.Yeet:
                    memoryCardAnimationManager.PlayOutAnimationYeet(currentMemoryCard.gameObject);
                    currentMemoryCardIndex++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return;
        }
    }
}

[Serializable]
public class MemoryCardInfo
{
    public bool isSpawned;
    public Sprite sprite;
    public OutAnimation outAnimation;
    public uint scoreIn;
    public uint scoreOut;
}

public enum OutAnimation
{
    Normal,
    Yeet
}