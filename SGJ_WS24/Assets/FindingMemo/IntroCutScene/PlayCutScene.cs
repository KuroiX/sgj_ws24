using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace FindingMemo.IntroCutScene
{
    public class PlayCutScene : MonoBehaviour
    {
        private VideoPlayer videoPlayer;

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();
            StartCutScene();
        }

        private void StartCutScene()
        {
            videoPlayer.Play();
            print("video started playing");
            StartCoroutine(nameof(CheckVideoPlaying));
        }

        private IEnumerator CheckVideoPlaying()
        {
            yield return new WaitForSeconds((float) videoPlayer.clip.length);
            while (videoPlayer.isPlaying) yield return null;
            OnVideoFinishedPlaying();
        }

        private void OnVideoFinishedPlaying()
        {
            print("video finished playing");
        }
    }
}