using UnityEngine;
using UnityEngine.Events;

public class PlayerResurces : MonoBehaviour
{
    // Ссылка на UI для передачи данных и дальнейшего обновления UI
    [SerializeField] private GamePlayUI _gamePlayUI = null;
    private int _coins = 0;
    private int _lives = 3;

    private delegate void Changed(int value);
    private event Changed LivesChanged;
    private event Changed CoinsChanged;

    public UnityEvent PlayerDied;

    private void Start()
    {
        LivesChanged += _gamePlayUI.LivesChangedHandler;
        CoinsChanged += _gamePlayUI.CoinsChangedHandler;
    }
        


    // Обрабатываем событие начала игры полученое от GameManager'a, устанавливаем значения по-умочанию
    public void GameStartedHandler()
    {
        _coins = PlayerPrefs.GetInt("coins", 0);
        _lives = 3;
    }

    // Обработка получения сразу множества койнов и дальнейшего обновления UI
    public void MultiCoinColected(int count)
    {
        _coins += count;
        CoinsChanged?.Invoke(_coins);
        PlayerPrefs.SetInt("coins", _coins);
    }
    // Обработка получения 1-го койна и обновление UI
    public void CoinCollectedHandler()
    {
        _coins++;
        CoinsChanged?.Invoke(_coins);
        PlayerPrefs.SetInt("coins", _coins);
    }
    // Обработка получения жизней ии обновление UI
    public void HeartCollectedHandler()
    {
        if (_lives < 3)
        {
            _lives++;
            LivesChanged?.Invoke(_lives);
        }
    }
    // Вычитание жизней при получении урона, если жизнb на 0  то отправляем событие о смерти в PlayersReaction
    public void DamageTakenHandler()
    {
        if (_lives > 0)
        {
            _lives--;
            LivesChanged?.Invoke(_lives);
        }
        if (_lives <= 0)
        {
            PlayerDied?.Invoke();
        }
    }
}
