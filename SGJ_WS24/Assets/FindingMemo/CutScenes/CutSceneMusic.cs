using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace FindingMemo.CutScenes
{
    public class CutSceneMusic : MonoBehaviour
    {
        [SerializeField] private EventReference cutSceneMusic;

        private EventInstance instance;

        public void StartMusic()
        {
            instance = RuntimeManager.CreateInstance(cutSceneMusic);
            instance.start();
        }

        public void StopMusic()
        {
            instance.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}