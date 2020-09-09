using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    public GameObject pathPrefab;
    public LevelType type;
    public List<GameObject> ObstaclesPrefabs;
    public List<GameObject> PowerUpPrefabs;
    public Vector3 finish;
    
    public enum LevelType
    {
        Streight=0,
        Corner=1
    }
}
