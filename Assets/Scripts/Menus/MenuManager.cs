using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    
    // Universal Menu Functions
    public void QuitGame()
    {
        Application.Quit();
    }

    // Title Menu Functions 
    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    // Pause Menu Functions
    public void PauseGame()
    {
        Time.timeScale = 0;
        _menu.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        _menu.SetActive(false);
    }
    public void TitleMenu()
    {
        SceneManager.LoadScene(0);
    }


    
}
