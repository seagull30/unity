using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI _bestTimeUI;
    // Start is called before the first frame update
   
    public void UpdateBestTime(int bestTime)
    {
        _bestTimeUI.text = $"Best Score : {bestTime}sec";
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
