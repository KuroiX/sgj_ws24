using UnityEngine;

namespace FindingMemo.CutScenes
{
    public class PlayCutScene : MonoBehaviour
    {
        [SerializeField] private bool playOnAwake = true;
        [SerializeField] private SceneLoader sceneLoader;

        private Animator animator;
        private static readonly int Start = Animator.StringToHash("Start");

        private void Awake()
        {
            animator = GetComponent<Animator>();
            if (playOnAwake) StartCutScene();
        }

        [ContextMenu("Start Cut Scene")]
        public void StartCutScene()
        {
            animator.SetTrigger(Start);
        }


        private void OnVideoFinishedPlaying()
        {
            print("video finished playing");
            sceneLoader.LoadNextScene();
        }
    }
}