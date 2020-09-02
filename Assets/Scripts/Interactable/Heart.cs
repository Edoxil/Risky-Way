﻿using DG.Tweening;
using UnityEngine;

public class Heart : MonoBehaviour, Iinteractable
{
    private BoxCollider _boxColider;
    [SerializeField] private ParticleSystem _particalSystem = null;

    private void Start()
    {
        _boxColider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        Vector3 rot = transform.eulerAngles;
        rot.y += 2f;

        transform.DOLocalRotate(rot, 0.1f);
    }


    public void Interact()
    {
        Die();
    }

    private void Die()
    {
        _boxColider.enabled = false;
        _particalSystem.Play();
        transform.DOMoveY(2.5f, 0.5f);
        transform.DOShakeScale(0.7f, 1f, 10, 1f);
        Destroy(gameObject, 1f);
    }

}