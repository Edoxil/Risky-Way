using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance = null;
    public static GameManager GetInstance()
    {
        return _instance;
    }
    void Awake()
    {

        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion Singleton


    public List<LevelData> Levels; // Список информации об уровнях
    [HideInInspector]
    public LevelData _levelData = null;

    // Префаб объекта в котором создается уровень (путь, препятствия,паверапы)
    [Space]
    [SerializeField] private GameObject _levelCreatorPrefab = null;
    // Обьект для инстанса префаба (удаляется каждый уровень очищая сцену)
    private GameObject _levelCreator = null;


    [SerializeField] private PlayerMovement _playerMovement = null;
    private CameraManager _cameraManager = null;


    public UnityEvent Started;
    public UnityEvent Finished;


    private void Start()
    {
        _cameraManager = CameraManager.GetInstance();
        Initialize();
    }


    private void Initialize()
    {
        if (_levelCreator != null)
        {
            Destroy(_levelCreator, 0.3f);
        }
        int curentLevel = PlayerPrefs.GetInt("level");

        if (curentLevel < 5)
        {
            _levelData = Levels[Random.Range(0, 2)];
        }
        if (curentLevel > 5 && curentLevel < 10)
        {
            _levelData = Levels[Random.Range(2, 5)];
        }
        if (curentLevel > 10 && curentLevel < 15)
        {
            _levelData = Levels[Random.Range(5, 8)];
        }
        if (curentLevel > 15 && curentLevel < 20)
        {
            _levelData = Levels[Random.Range(8, 11)];
        }
        if (curentLevel > 20)
        {
            _levelData = Levels[Random.Range(11, 12)];
        }

        _levelCreator = Instantiate(_levelCreatorPrefab, transform.position, Quaternion.identity);
        if (_levelCreator != null)
        {
            PlayerPrefs.SetInt("lives", 3);
            Started?.Invoke();
            _playerMovement.SetFinish(_levelData.finish);
        }
    }


    // Обработка нажатия Tap To Start кнопки
    public void StartGame()
    {
        _playerMovement.isStoped = false;
    }

    public void RestartGame()
    {
        Transform target = _playerMovement.GetComponent<Transform>();
        _cameraManager.StartFolowing(target);

        Initialize();
    }
    public void NextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("level");
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);

        Initialize();
    }


    // Вызываем событие на финеше и показываем Win Screen
    public void Finish()
    {
        Finished?.Invoke();
    }
}