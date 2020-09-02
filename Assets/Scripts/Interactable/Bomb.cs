using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour, Iinteractable
{
    [SerializeField] private ParticleSystem _explosion = null;
    private BoxCollider _boxColider;
    private Renderer _renderer;

    public UnityEvent Explode;

    private void Start()
    {
        _boxColider = GetComponent<BoxCollider>();
        _renderer = GetComponent<Renderer>();
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
        transform.DOScale(1.5f, 0.15f);
        Explode?.Invoke();
        _explosion.Play();
        _renderer.enabled = false;
        Destroy(gameObject, 1f);
    }

}
