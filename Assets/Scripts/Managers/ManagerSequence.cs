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
    private GameObject bff;
    [SerializeField]
    private TextAsset anxLoss, rowWin;

    bool changeScene, loseCondition, winCondition, startCondition;

    public event Action HouseInitialize;
    // Start is called before the first frame update
    void Start()
    {
        changeScene = loseCondition = winCondition = startCondition = false;
        FindFirstObjectByType<ManagerAnxiety>().AnxietyLoss += AnxietyLossSequence;
        FindFirstObjectByType<ManagerTime>().Midnight += RowanWin;
        FindFirstObjectByType<ManagerInk>().FinishedTalking += FinishedTalk;
        OpeningSequence();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount < 5 && Time.frameCount > 0)
        {
            player.Interact();
        }
    }

    private void OpeningSequence()
    {
        player.transform.rotation = Quaternion.Euler(0, 90, 0);
        startCondition = true;
        lightsOutside.SetActive(true);
        lightsInside.SetActive(false);
    }

    private void GetInHouse()
    {
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
        player.TalkSequence(anxLoss);
    }

    private void GoToLossScreen()
    {
        SceneManager.LoadScene("LoseEnd");
    }

    private void RowanWin()
    {
        winCondition = true;
        player.transform.position = rowanWin.position;
        player.transform.rotation = rowanWin.rotation;
        player.TalkSequence(rowWin);
    }

    private void GoToWinScreen()
    {
        SceneManager.LoadScene("WinEnd");
    }

    private void FinishedTalk()
    {
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
