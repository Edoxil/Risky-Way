using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MetalCrate : MonoBehaviour, Iinteractable
{
    private BoxCollider _boxColider;
    public UnityEvent ReturnDamage;

    private void Start()
    {
        _boxColider = GetComponent<BoxCollider>();
    }
    public void Interact()
    {
        _boxColider.enabled = false;
        ReturnDamage?.Invoke();
    }
    private void OnColissionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Interact();
        }
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(2f);
        _boxColider.enabled = true;
    }
}
