using System;
using UnityEngine;

namespace Save
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseUI;

        private bool isPaused;

        public void Start()
        {
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