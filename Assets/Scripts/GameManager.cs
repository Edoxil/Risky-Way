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

    // Префаб объекта в котором создается уровень (путь, препятствия,поверапы)
    [SerializeField] private GameObject _levelCreatorPrefab = null;
    private GameObject _levelCreator = null;

    [SerializeField] private PlayerMovement _playerMovement = null;
    private CameraManager _cameraManager = null;

    public UnityEvent Started;
    public UnityEvent Finished;
    

    private void Start()
    {
        // TMP
        PlayerPrefs.SetInt("level", 0);
        

        _cameraManager = CameraManager.GetInstance();
        _levelCreator = Instantiate(_levelCreatorPrefab, transform.position, Quaternion.identity);
        _playerMovement.isStoped = true;
        _playerMovement.SetDefoultPosition();
        Started?.Invoke();
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

        PlayerPrefs.SetInt("lives", 3);
        Destroy(_levelCreator);
        _levelCreator = Instantiate(_levelCreatorPrefab, transform.position, Quaternion.identity);
        _playerMovement.isStoped = true;
        _playerMovement.SetDefoultPosition();
        Started?.Invoke();
    }

  
    public void NextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("level");
        currentLevel++;
        PlayerPrefs.SetInt("level", currentLevel);
        Destroy(_levelCreator);
        _levelCreator = Instantiate(_levelCreatorPrefab, transform.position, Quaternion.identity);
        _playerMovement.isStoped = true;
        _playerMovement.SetDefoultPosition();
        Started?.Invoke();
    }

    // Вызываем событие на финеше и показываем Win Screen
    public void Finish()
    {
        _playerMovement.isStoped = true;
        Finished?.Invoke();
    }
} 