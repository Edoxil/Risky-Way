using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CheckPointTrigger : MonoBehaviour
{
    [SerializeField] private Transform _flagTransform = null;
    private BoxCollider _boxCollider = null;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerMovement>(out PlayerMovement _))
        {
            other.GetComponentInChildren<PlayerReaction>().SetCheckPoint(transform.position);
            _boxCollider.enabled = false;
            Vector3 rot = _flagTransform.localEulerAngles;
            rot.y -= 90f;
            _flagTransform.DORotate(rot, 0.1f);
        }
    }
}
