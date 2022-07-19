using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 1. ������ ����Ǹ� GameOverUI�� �����ش�
    //2, ����� �ȳ��� ���ش�.

    public TimerText TimerTextUI;
    public GameObject GameOverUI;

    private bool _isOver;

    private void Update()
    {
        //���࿡ ������ ���ᰡ �Ǿ��ٸ�
        // RŰ�� �����ٸ� �����
        if (_isOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);

        }
    }

    public void End()
    {
        // Ÿ�̸� ����
        TimerTextUI.IsOn = false;

        // ������ ����
        int savedBestTime = PlayerPrefs.GetInt("bestTime");
        int bestTime = Math.Max(TimerTextUI.SurvivalTime, savedBestTime);
            
        PlayerPrefs.SetInt("bestTime", bestTime);

        // GameOverUI���ٰ� ����
        GameOverUI.SetActive(true);
        GameOverUI.GetComponent<GameOverUI>().UpdateBestTime(bestTime);

        // _isOver = true;
        _isOver = true;
    }
}
