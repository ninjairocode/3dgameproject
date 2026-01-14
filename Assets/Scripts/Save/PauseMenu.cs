using System;
using Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Save
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseUI;
        
        [Header("Audio Sliders")]
        public Slider ambienceSlider;
        public Slider sfxSlider;


        private bool isPaused;

        public void Start()
        {
            ambienceSlider.value = SoundManager.Instance.ambienceVolume;
            sfxSlider.value = SoundManager.Instance.sfxVolume;

            ambienceSlider.onValueChanged.AddListener(SoundManager.Instance.SetAmbienceVolume);
            sfxSlider.onValueChanged.AddListener(SoundManager.Instance.SetSFXVolume);

            pauseUI.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
                if (isPaused)
                {
                    pauseUI.SetActive(true);
                }
                else
                {
                    pauseUI.SetActive(false);
                }
            }
        }

        public void TogglePause()
        {
            isPaused = !isPaused;
            pauseUI.SetActive(isPaused);

            Time.timeScale = isPaused ? 0 : 1;
        }

        public void OnSaveButton()
        {
            if (SaveManager.Instance == null)
            {
                Debug.LogError("SaveManager.Instance está NULL!");
                return;
            }

            SaveManager.Instance.SaveGame();


        }

        public void OnLoadButton()
        {
            SaveManager.Instance.LoadGame();

        }
    }
}