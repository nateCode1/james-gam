using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject cameraControl;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ExitGame(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sensitivitySlider;
    
    private void Awake ()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            SetVolume(PlayerPrefs.GetFloat("Volume"));
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            cameraControl.GetComponent<CameraController>().SetSensitivity(PlayerPrefs.GetFloat("Sensitivity"));
            sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        }
    }
    public void SetVolume (float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetSensitivity (float sensitivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }
}
