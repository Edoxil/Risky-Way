using DG.Tweening;
using UnityEngine;

public class MetalCrate : MonoBehaviour, Iinteractable
{
    public void Interact()
    {
        transform.DOShakeRotation(0.2f, 5f, 3);
    }
}