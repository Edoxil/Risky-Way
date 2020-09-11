using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour, Iinteractable
{

    private BoxCollider _boxCollider = null;
    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
    public void Interact()
    {
        _boxCollider.enabled = false;
        transform.DOScaleY(5f, 0.2f).onComplete += () =>
          { transform.DOScaleY(1f, 0.2f); };
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        _boxCollider.enabled = true;
        StopAllCoroutines();
    }
}
