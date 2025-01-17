﻿using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour, Iinteractable
{
    private BoxCollider _boxColider;
    private float _rotSpeed = 2f;
    public bool isRotated = true;

    private void Start()
    {
        _boxColider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (transform == null) { return; }
        if (! isRotated) { return; }
        Vector3 rot = transform.localEulerAngles;
        rot.y += _rotSpeed;

        transform.DOLocalRotate(rot, 0.1f);
    }


    public void Interact()
    {
        Die();
    }

    private void Die()
    {
        isRotated = true;
        _rotSpeed = 7f;
        _boxColider.enabled = false;
        transform.DOMoveY(4f, 0.5f);
        transform.DOShakeScale(0.7f, 1f, 10, 1f);
        
        Destroy(gameObject, 1f);
    }
}