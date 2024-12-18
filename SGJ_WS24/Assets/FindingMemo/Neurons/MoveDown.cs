using UnityEngine;

namespace FindingMemo.Neurons
{
    public class MoveDown : MonoBehaviour
    {
        [SerializeField] public float speed;

        private bool shouldScroll;

        public void StartScrolling()
        {
            shouldScroll = true;
        }


        protected void Update()
        {
            if (!shouldScroll) return;

            transform.position += Vector3.down * (speed * Time.deltaTime);
        }
    }
}