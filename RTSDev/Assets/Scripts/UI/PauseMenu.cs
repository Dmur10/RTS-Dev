using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RTSGame.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public static bool IsPaused;
        public GameObject PauseMenuUI;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (IsPaused)
                {
                    Resume();
                } else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            IsPaused = false;
        }

        public void Pause()
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            IsPaused = true;
        }

        public void Save()
        {
            //
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
