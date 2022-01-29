using UnityEngine;

namespace Util
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource backgroundAudioSource;
        [SerializeField] private AudioClip[] backgroundMusicForDays;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip buttonClick;
        [SerializeField] private AudioClip textReadout;

        public enum SoundClip
        {
            ButtonClick,
            TextReadout
        }
        

        private static SoundManager _instance;

        public static SoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<SoundManager>();
                }

                return _instance;
            }
        }

        public void runBackgroundForDay(int day)
        {
            backgroundAudioSource.clip = backgroundMusicForDays[day];
            backgroundAudioSource.Play();
        }

        public void playsoundEffect(SoundClip soundClip)
        {
            switch (soundClip)
            {
                case SoundClip.ButtonClick:
                    audioSource.PlayOneShot(buttonClick);
                    break;
                case SoundClip.TextReadout:
                    audioSource.clip = textReadout;
                    audioSource.Play();
                    break;

            }
        }
    }
}