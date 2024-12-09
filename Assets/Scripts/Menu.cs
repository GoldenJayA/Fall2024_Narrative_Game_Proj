using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject creditsObj;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        if(creditsObj != null )
        {
            creditsObj.SetActive( false );
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Environment");
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

