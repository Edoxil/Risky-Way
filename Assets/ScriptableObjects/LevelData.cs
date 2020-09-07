using UnityEngine;

[CreateAssetMenu(menuName = "SriptableObjects/LevelData")]
public class LevelData : ScriptableObject
{
    public int number;
    public GameObject pathPrefab;
    public LevelType type;

    public enum LevelType
    {
        Streight=0,
        Corner=1
    }
}
