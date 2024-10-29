using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    public class SoundManager : MonoBehaviour
    {
        static SoundManager instance;
        public static SoundManager Instance
        {
            get { return instance; }
        }

        [SerializeField]
        AudioSource soundEffect;

        [SerializeField]
        AudioSource soundMusic;

        AudioSource footstep;

        bool isMute;
        float volume;

        public List<SoundType> sounds;

        void Awake ()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start ()
        {
            PlayMusic(ESound.Music);
        }

        public void ToggleMute (bool status)
        {
            isMute = status;
        }

        public void SetVolume (float newVolume)
        {
            volume = newVolume;
            soundEffect.volume = volume;
            soundMusic.volume = volume;
        }

        public void Play (ESound sound)
        {
            if (isMute)
            {
                return;
            }
            AudioClip audioClip = GetSoundClip(sound);
            if (audioClip != null)
            {
                soundEffect.PlayOneShot(audioClip);
            }
            else
            {
                Debug.LogError($"Clip not found for sound type {sound.ToString()}");
            }
        }

        public void PlayMusic (ESound sound)
        {
            if (isMute)
            {
                return;
            }
            AudioClip audioClip = GetSoundClip(sound);
            if (audioClip != null)
            {
                soundMusic.clip = audioClip;
                soundMusic.Play();
            }
            else
            {
                Debug.LogError($"Clip not found for sound type {sound.ToString()}");
            }
        }

        private AudioClip GetSoundClip (ESound sound)
        {
            var soundType = sounds.Find(x => x.soundType == sound);
            if (soundType != null)
            {
                return soundType.soundClip;
            }
            else
            {
                return null;
            }
        }
    }
}
