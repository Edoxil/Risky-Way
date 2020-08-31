using DG.Tweening;
using UnityEngine;

public class DirLight : MonoBehaviour
{
    public void LeftTurnHandler()
    {
        Vector3 rot = new Vector3(50f, 180f, 0f);
        transform.DORotate(rot, 2f);
    }
}
