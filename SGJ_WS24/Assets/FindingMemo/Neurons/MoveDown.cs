using UnityEngine;

namespace FindingMemo.Neurons
{
    public class MoveDown : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void Update()
        {
            transform.position += Vector3.down * (speed * Time.deltaTime);
        }
    }
}