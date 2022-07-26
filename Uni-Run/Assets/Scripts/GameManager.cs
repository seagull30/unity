using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
// 싱글톤 패턴

public class GameManager : SingletonBehaviour<GameManager>
{
    // 옵저버 패턴
    //public event UnityAction OnGameEnd2;
    public UnityEvent OnGameEnd = new UnityEvent();
    public UnityEvent<int> OnScoreChanged = new UnityEvent<int>();

    public int ScoreIncreaseAmount = 1;
    public int CurrnetScore
    {
        get
        {
            return _currnetScore;
        }
        set
        {
            _currnetScore = value;
            OnScoreChanged.Invoke(_currnetScore);
        }
    }

    private int _currnetScore = 0; // 게임 점수
    private bool _isEnd = false; // 게임 오버 상태

    void Update()
    {
        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if (_isEnd && Input.GetKeyDown(KeyCode.R))
        {
            reset();
            SceneManager.LoadScene(0);
        }
    }

    // 점수를 증가시키는 메서드
    public void AddScore()
    {
        CurrnetScore += ScoreIncreaseAmount;
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void End()
    {
        _isEnd = true;
        OnGameEnd.Invoke();
        //OnGameEnd2?.Invoke();
    }

    private void reset()
    {
        _currnetScore = 0;
        _isEnd = false;
    }
}