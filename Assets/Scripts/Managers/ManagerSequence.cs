using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ManagerSequence : MonoBehaviour
{
    [SerializeField]
    private Transform playerHouseStart, bffHouseStart, rowanWin, anxietyLose;
    [SerializeField]
    private GameObject lightsOutside, lightsInside;
    [SerializeField]
    private FirstPersonPlayer player;
    [SerializeField]
    private Camera mainCam;
    [SerializeField]
    private GameObject bff, rowan;
    [SerializeField]
    private TextAsset anxLoss, rowWin;

    bool changeScene, loseCondition, winCondition, startCondition;
    private float startTime;

    public event Action HouseInitialize;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        changeScene = loseCondition = winCondition = startCondition = false;
        FindFirstObjectByType<ManagerAnxiety>().AnxietyLoss += AnxietyLossSequence;
        FindFirstObjectByType<ManagerTime>().Midnight += RowanWin;
        FindFirstObjectByType<ManagerInk>().FinishedTalking += FinishedTalk;
        player.transform.position = anxietyLose.position;
        player.transform.rotation = anxietyLose.rotation;
        OpeningSequence();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime < 0.5)
        {
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            mainCam.transform.localRotation = Quaternion.Euler(0, 0, 0);
            player.Interact();
        }

        Debug.Log("win condition: " + winCondition);
        Debug.Log("lose condition: " + loseCondition);
        Debug.Log("start condition: " + startCondition);
    }

    private void OpeningSequence()
    {
        player.transform.rotation = Quaternion.Euler(0, 90, 0);
        mainCam.transform.localRotation = Quaternion.Euler(0, 0, 0);
        startCondition = true;
        lightsOutside.SetActive(true);
        lightsInside.SetActive(false);
    }

    private void GetInHouse()
    {
        rowan.SetActive(false);
        lightsOutside.SetActive(false);
        lightsInside.SetActive(true);
        bff.GetComponent<NavMeshAgent>().enabled = false;
        player.transform.position = playerHouseStart.position;
        player.transform.rotation = playerHouseStart.rotation;
        bff.transform.position = bffHouseStart.position;
        bff.transform.rotation = bffHouseStart.rotation;
        bff.GetComponent<NavMeshAgent>().enabled = true;
        HouseInitialize?.Invoke();
    }

    private void AnxietyLossSequence()
    {
        loseCondition = true;
        lightsOutside.SetActive(true);
        lightsInside.SetActive(false);
        player.transform.position = anxietyLose.position;
        player.transform.rotation = anxietyLose.rotation;
        mainCam.transform.localRotation = Quaternion.Euler(0, 0, 0);
        player.TalkSequence(anxLoss);
    }

    private void GoToLossScreen()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene("LoseEnd");
    }

    private void RowanWin()
    {
        rowan.SetActive(true);
        winCondition = true;
        player.transform.position = rowanWin.position;
        player.transform.rotation = rowanWin.rotation;
        mainCam.transform.localRotation = Quaternion.Euler(0, 0, 0);
        player.TalkSequence(rowWin);
    }

    private void GoToWinScreen()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene("WinEnd");
    }

    private void FinishedTalk()
    {
        Debug.Log("Finshed Talking. Variable status: \t startCondition: " + startCondition + "\t loseCondition: " + loseCondition + "\t winCondition: " + winCondition);
        changeScene = true;
        if(startCondition && changeScene)
        {
            GetInHouse();
        }
        else if(loseCondition && changeScene)
        {
            GoToLossScreen();
        }
        else if(winCondition && changeScene)
        {
            GoToWinScreen();
        }

        startCondition = loseCondition = winCondition = false;
    }

}
