using System;
using FMODUnity;
using UnityEngine;

namespace FindingMemo.CutScenes
{
    public class VoiceLineTriggerer : MonoBehaviour
    {
        [SerializeField] private EventReference[] voiceLines;

        private int counter = 0;

        public void PlayNextLine()
        {
            if (counter == voiceLines.Length) return;

            var instance = RuntimeManager.CreateInstance(voiceLines[counter]);
            instance.start();
            counter++;
        }
    }
}