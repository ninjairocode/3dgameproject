using System.Collections.Generic;
using EBAC.Core.Singleton;
using UnityEngine;

namespace Audio
{
    public class SoundManager : Singleton<SoundManager>
    {
        [Header("Audio Sources")]
        public AudioSource ambienceSource;
        public AudioSource sfxSource;

        [Header("Volumes")]
        [Range(0f, 1f)] public float ambienceVolume = 1f;
        [Range(0f, 1f)] public float sfxVolume = 1f;

        [System.Serializable]
        public class SFXGroup
        {
            public string id;
            public AudioClip clip;
        }

        [Header("Lista de SFX")]
        public List<SFXGroup> sfxList = new List<SFXGroup>();


        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);

            LoadVolumes();
            ApplyVolumes();
        }

        

        public void PlayAmbience(AudioClip clip)
        {
            if (clip == null) return;

            ambienceSource.clip = clip;
            ambienceSource.loop = true;
            ambienceSource.volume = ambienceVolume;
            ambienceSource.Play();
        }

        public void SetAmbienceVolume(float value)
        {
            ambienceVolume = value;
            ambienceSource.volume = ambienceVolume;
            SaveVolumes();
        }

        

        public void PlaySFX(string id)
        {
            foreach (var sfx in sfxList)
            {
                if (sfx.id == id)
                {
                    sfxSource.PlayOneShot(sfx.clip, sfxVolume);
                    return;
                }
            }

            Debug.LogWarning("SFX não encontrado: " + id);
        }
        
        public void PlayLoopSFX(AudioClip clip)
        {
            if (clip == null) return;

            // 🔥 Impede reiniciar o mesmo áudio em loop
            if (sfxSource.isPlaying && sfxSource.clip == clip)
                return;

            sfxSource.clip = clip;
            sfxSource.loop = true;
            sfxSource.volume = sfxVolume;
            sfxSource.Play();
        }

        public void StopLoopSFX()
        {
            sfxSource.loop = false;
            sfxSource.Stop();
        }

        public void SetSFXVolume(float value)
        {
            sfxVolume = value;
            sfxSource.volume = sfxVolume;
            SaveVolumes();
        }

        

        private void SaveVolumes()
        {
            PlayerPrefs.SetFloat("AmbienceVolume", ambienceVolume);
            PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        }

        private void LoadVolumes()
        {
            ambienceVolume = PlayerPrefs.GetFloat("AmbienceVolume", 1f);
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        }

        private void ApplyVolumes()
        {
            ambienceSource.volume = ambienceVolume;
            sfxSource.volume = sfxVolume;
        }
    }
}