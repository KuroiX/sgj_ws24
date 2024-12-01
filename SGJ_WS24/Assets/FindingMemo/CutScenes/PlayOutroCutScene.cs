using System;
using UnityEngine;

namespace FindingMemo.CutScenes
{
    public class PlayOutroCutScene : MonoBehaviour
    {
        [SerializeField] private GameObject outroGood;
        [SerializeField] private GameObject outroBad;
        private static readonly int Start = Animator.StringToHash("Start");

        private void Awake()
        {
            outroGood.SetActive(false);
            outroBad.SetActive(false);
        }


        [ContextMenu("Play outro bad")]
        public void PlayOutro()
        {
            PlayOutro(false);
        }


        [ContextMenu("Play outro good")]
        public void PlayOutro2()
        {
            PlayOutro(true);
        }


        public void PlayOutro(bool rememberedName)
        {
            var outro = rememberedName
                ? outroGood
                : outroBad;

            outro.SetActive(true);
            outro.GetComponent<Animator>().SetTrigger(Start);
        }
    }
}