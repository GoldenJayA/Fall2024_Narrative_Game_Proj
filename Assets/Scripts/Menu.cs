using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject creditsObj;
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ShowCredits()
    {
        creditsObj.SetActive(true);
    }

    public void HideCredits()
    {
        creditsObj.SetActive(false);
    }

}

