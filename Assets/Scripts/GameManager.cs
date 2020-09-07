using System.Collections.Generic;
using UnityEngine;


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

    public List<LevelData> Levels;
    [SerializeField] private GameObject _levelCreatorPrefab = null;

    [SerializeField] private PlayerMovement _playerMovement = null;


    private GameObject _levelCreator = null;

    private void Start()
    {
        _levelCreator = Instantiate(_levelCreatorPrefab, transform.position, Quaternion.identity);
        _playerMovement.isStoped = true;
        _playerMovement.SetDefoultPosition();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {

            RestartGame();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {

            NextLevel();
        }

    }

    public void StartGame()
    {
        // TMP
        PlayerPrefs.SetInt("currentLevel", 0);


        _levelCreator = Instantiate(_levelCreatorPrefab, transform.position, Quaternion.identity);
        _playerMovement.isStoped = true;
        _playerMovement.SetDefoultPosition();

    }

    public void RestartGame()
    {
       
        Destroy(_levelCreator);
        _levelCreator = Instantiate(_levelCreatorPrefab, transform.position, Quaternion.identity);
        _playerMovement.isStoped = true;
        _playerMovement.SetDefoultPosition();


    }


    public void NextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel");
        currentLevel++;
        PlayerPrefs.SetInt("currentLevel", currentLevel);
        Destroy(_levelCreator);
        StartGame();
    }

}


