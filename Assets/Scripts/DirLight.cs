using DG.Tweening;
using UnityEngine;

public class DirLight : MonoBehaviour
{
    // После смены оси движения поворачиваем  свет 
    public void TurningHandler()
    {
        Vector3 rot = new Vector3(-315f, -130f, 0f);
        transform.DORotate(rot, 2f);
    }
    public void GameStartHandler()
    {
        Vector3 rot = new Vector3(50f, -45f, 0f);
        transform.DORotate(rot, 2f);
    }
}
