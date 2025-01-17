﻿using UnityEngine;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private Text _livesCount = null;
    [SerializeField] private Text _coinsCount = null;
    [SerializeField] private Text _curentLevel = null;
    [SerializeField] private Text _nextLevel = null;
    [SerializeField] private Slider _progressBar = null;

    // Устанавливаем значения UI элементов при старте 
    public void GameStartedHandler()
    {
        int lvl = PlayerPrefs.GetInt("level");
        _curentLevel.text = lvl.ToString();
        _nextLevel.text = (lvl + 1).ToString();
        _livesCount.text = PlayerPrefs.GetInt("lives", 3).ToString();
        _coinsCount.text = PlayerPrefs.GetInt("coins", 0).ToString();
        _progressBar.maxValue = 0;
        _progressBar.minValue = 0;
    }
    
     // Методы обновления UI    
    public void SetProgressBarValue(float value)
    {
        if(_progressBar.minValue == 0)
        {
            _progressBar.minValue = -value;
        }
        _progressBar.value = -value;
    }
    public void LivesChangedHandler(int lives)
    {
        _livesCount.text = lives.ToString();
    }
    public void CoinsChangedHandler(int coins)
    {
        _coinsCount.text = coins.ToString();
    }
}