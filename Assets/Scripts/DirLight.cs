using DG.Tweening;
using UnityEngine;

public class DirLight : MonoBehaviour
{
    // После смены оси движения поворачиваем  свет 
    public void TurnLeftHandler()
    {
        Vector3 rot = new Vector3(50f, 180f, 0f);
        transform.DORotate(rot, 2f);
    }
}
