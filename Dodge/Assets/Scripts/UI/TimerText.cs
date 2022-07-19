using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerText : MonoBehaviour
{
    // 초를 기록한다.
    public int SurvivalTime { get; private set; }
    private TextMeshProUGUI _ui;
    private float _elapsedTime;
    public bool IsOn = true;
    void Start()
    {
        _ui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= 1f)
        {
            ++SurvivalTime;
            _elapsedTime = 0f;
            _ui.text = $"TIme : {SurvivalTime}sec";
        }
    }
}
