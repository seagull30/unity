using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 1. 게임이 종료되면 GameOverUI를 보여준다
    //2, 재시작 안내를 해준다.

    public TimerText TimerTextUI;
    public GameObject GameOverUI;

    private bool _isOver;

    private void Update()
    {
        //만약에 게임이 종료가 되었다면
        // R키를 눌렀다면 재시작
        if (_isOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);

        }
    }

    public void End()
    {
        // 타이머 종료
        TimerTextUI.IsOn = false;

        // 데이터 저장
        int savedBestTime = PlayerPrefs.GetInt("bestTime");
        int bestTime = Math.Max(TimerTextUI.SurvivalTime, savedBestTime);
            
        PlayerPrefs.SetInt("bestTime", bestTime);

        // GameOverUI에다가 갱신
        GameOverUI.SetActive(true);
        GameOverUI.GetComponent<GameOverUI>().UpdateBestTime(bestTime);

        // _isOver = true;
        _isOver = true;
    }
}
