using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    private ObjectsGenerator _generator = null;
    private GameObject _pathPrefab = null;
    private GameManager _gameManager = null;
    private int _currentLevel => PlayerPrefs.GetInt("level", 0);


    void Start()
    {
        _gameManager = GameManager.GetInstance();
        _generator = GetComponent<ObjectsGenerator>();
        _pathPrefab = _gameManager.Levels[_currentLevel].pathPrefab;

        Instantiate(_pathPrefab, transform.position, Quaternion.identity, transform);
        _generator.Generate(_gameManager.Levels[_currentLevel].obstaclesPrefabs);
    }
   
}