using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SettingObjects
{
    [CreateAssetMenu(fileName = "AudioClips", menuName = "ScriptableObjects/AudioClips")]
    public class AudioClips : ScriptableObject
    {
        [Serializable]
        public struct KeyAudio
        {
            public string key;
            public AudioClip[] audio;
        }

        [SerializeField]
        private KeyAudio[] audios;

        public Dictionary<string, AudioClip[]> CreateKeyAudioDictionary()
        {
            return audios.ToDictionary(t => t.key, t => t.audio);
        }
    }
}