using System.Collections;
using UnityEngine;

public class MetalCrate : MonoBehaviour, Iinteractable
{
    private BoxCollider _boxColider;


    private void Start()
    {
        _boxColider = GetComponent<BoxCollider>();
    }
    public void Interact()
    {
        _boxColider.enabled = false;
        StartCoroutine(Cooldown());
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(3f);
        _boxColider.enabled = true;
        StopAllCoroutines();
    }
}