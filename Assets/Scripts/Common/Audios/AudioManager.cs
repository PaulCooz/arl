using UnityEngine;

namespace Common.Audios
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource source;

        public AudioSource Play(AudioClip clip, float volume)
        {
            source.clip = clip;
            source.volume = volume;
            source.Play();

            Destroy(gameObject, clip.length);

            return source;
        }
    }
}