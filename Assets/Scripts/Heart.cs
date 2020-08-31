using DG.Tweening;
using UnityEngine;

public class Heart : MonoBehaviour, Iinteractable
{
    private BoxCollider _boxColider;
    [SerializeField] private ParticleSystem _particalSystem;
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
        Debug.Log("Interact with heart");
        _boxColider.enabled = false;
        Die();
        Destroy(gameObject,1f);
        
    }

  
    private void Die()
    {
        _particalSystem.Play();
        transform.DOMoveY(2.5f, 0.5f);
        transform.DOShakeScale(0.7f,1f, 10, 1f);
    }
}


