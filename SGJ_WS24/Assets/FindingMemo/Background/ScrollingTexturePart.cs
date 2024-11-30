using FindingMemo.Neurons;
using UnityEngine;

namespace FindingMemo.Background
{
    public class ScrollingTexturePart : MoveDown
    {
        [HideInInspector] public float yPositionToMoveTextureUp;
        private ScrollingTextureManager scrollingTextureManager;

        private void Awake()
        {
            scrollingTextureManager = transform.parent.GetComponent<ScrollingTextureManager>();
        }

        private new void Update()
        {
            base.Update();

            if (transform.position.y <= yPositionToMoveTextureUp)
            {
                scrollingTextureManager.MoveTextureUp(transform);
            }
        }
    }
}