using UnityEngine;

namespace FindingMemo.Neurons
{
    public class MoveDown : MonoBehaviour
    {
        [SerializeField] public float speed;

        protected void Update()
        {
            transform.position += Vector3.down * (speed * Time.deltaTime);
        }
    }
}