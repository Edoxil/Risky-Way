using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    private GameObject _pathPrefab = null;
    private GameManager _gameManager = null;
    private int _currentLevel => PlayerPrefs.GetInt("currentLevel", 0);


    void Start()
    {
        _gameManager = GameManager.GetInstance();
        _pathPrefab = _gameManager.Levels[_currentLevel].pathPrefab;
        Instantiate(_pathPrefab, transform.position, Quaternion.identity, transform);
    }
   
}