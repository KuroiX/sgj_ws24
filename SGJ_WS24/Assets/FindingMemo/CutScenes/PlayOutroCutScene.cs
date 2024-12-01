using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindingMemo.CutScenes
{
    public class PlayOutroCutScene : MonoBehaviour
    {
        [SerializeField] private GameObject outroGood;
        [SerializeField] private GameObject outroBad;
        private static readonly int Start2 = Animator.StringToHash("Start");

        [SerializeField] private float time;
        

        private void Awake()
        {
            outroGood.SetActive(false);
            outroBad.SetActive(false);
        }


        [ContextMenu("Play outro bad")]
        public void PlayBadOutro()
        {
            PlayOutro(false);
        }


        [ContextMenu("Play outro good")]
        public void PlayGoodOutro()
        {
            PlayOutro(true);
        }

        public IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(time);
            FindObjectOfType<SceneLoader>().LoadSceneByIndex(0);
        }
        
        public void PlayOutro(bool rememberedName)
        {
            var outro = rememberedName
                ? outroGood
                : outroBad;

            outro.SetActive(true);
            outro.GetComponent<Animator>().SetTrigger(Start2);
            
            StartCoroutine(LoadScene());
        }
    }
}