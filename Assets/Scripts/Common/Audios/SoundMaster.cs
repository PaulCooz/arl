using System.Linq;
using UnityEngine;

namespace Common.Audios
{
    [CreateAssetMenu]
    public class SoundMaster : ScriptableObject
    {
        [SerializeField]
        private AudioManager audioPrefab;
        [SerializeField]
        private Pair<string, AudioClip>[] audio;

        private static SoundMaster _instance;

        public static SoundMaster Instance
        {
            get
            {
                if (_instance == null) _instance = Resources.Load<SoundMaster>("SoundMaster");
                return _instance;
            }
        }

        public AudioSource PlayOnce(string key, in float volume = 1f)
        {
            var clip = audio.First(e => e.key == key).value;
            var source = Instantiate(audioPrefab);

            return source.Play(clip, volume);
        }
    }
}