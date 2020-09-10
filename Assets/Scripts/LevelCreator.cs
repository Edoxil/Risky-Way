using UnityEngine;

[RequireComponent(typeof(ObjectsGenerator))]
public class LevelCreator : MonoBehaviour
{
    private ObjectsGenerator _generator = null;
    private GameObject _pathPrefab = null;
    private GameManager _gameManager = null;
    private LevelData _levelData = null;
  

    
    void Awake()
    {
        _gameManager = GameManager.GetInstance();
        _generator = GetComponent<ObjectsGenerator>();
      
        _levelData = _gameManager._levelData;
        _pathPrefab = _levelData.pathPrefab;

        Instantiate(_pathPrefab, transform.position, Quaternion.identity, transform);
        _generator.Generate(_levelData);
    }
   
}