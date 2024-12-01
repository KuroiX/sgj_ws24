using UnityEngine;

namespace FindingMemo.Background
{
    public class ScrollingTextureManager : MonoBehaviour
    {
        [SerializeField] private float yPositionToMoveTextureUp;
        [SerializeField] private float speed = 10;
        private Transform currentLastTexture;
        private ScrollingTexturePart[] scrollingTextureParts;

        private void Awake()
        {
            scrollingTextureParts = transform.GetComponentsInChildren<ScrollingTexturePart>();
            float yPos = int.MinValue;

            foreach (var scrolling in scrollingTextureParts)
            {
                if (scrolling.transform.position.y > yPos)
                {
                    yPos = scrolling.transform.position.y;
                    currentLastTexture = scrolling.transform;
                }

                scrolling.yPositionToMoveTextureUp = yPositionToMoveTextureUp;
                scrolling.speed = speed;
            }
        }


        public void MoveTextureUp(Transform scrollingTexturePart)
        {
            scrollingTexturePart.position = new Vector3(scrollingTexturePart.position.x,
                currentLastTexture.position.y + 10,
                scrollingTexturePart.position.z);
            currentLastTexture = scrollingTexturePart;
        }

        public void StartBackgroundScroll()
        {
            foreach (var scrolling in scrollingTextureParts)
            {
                scrolling.StartScrolling();
            }
        }
    }
}