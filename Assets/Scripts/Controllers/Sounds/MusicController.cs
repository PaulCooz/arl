using System.Collections.Generic;
using Libs;
using SettingObjects;
using SettingObjects.Keys;
using UnityEngine;

namespace Controllers.Sounds
{
    public class MusicController : MonoBehaviour
    {
        private const float MusicSwapDuration = 0.4f;

        private WaitForSeconds _musicSwap;
        private Dictionary<string, AudioClip[]> _clips;

        [SerializeField]
        private AudioClips audioClipsObject;
        [SerializeField]
        private AudioSource audioSource;

        private void Awake()
        {
            _clips = audioClipsObject.CreateKeyAudioDictionary();
            _musicSwap = new WaitForSeconds(MusicSwapDuration);
        }

        private void Start()
        {
            Play(SoundKeys.GameBackground);
        }

        public void Play(in string soundKey)
        {
            audioSource.clip = _clips[soundKey].Rand();
            audioSource.Play();
        }
    }
}