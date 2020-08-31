using DG.Tweening;
using UnityEngine;

public class Bomb : MonoBehaviour, Iinteractable
{

    private void Update()
    {
        Vector3 rot = transform.eulerAngles;
        rot.y+=2f;

        transform.DOLocalRotate(rot, 0.1f);

    }
    public void Interact()
    {
        Debug.Log("Interact with bomb");
        Die();
        Destroy(gameObject, 0.3f);
        
    }
       

    private void Die()
    {
        transform.DOScale(1.5f, 0.15f);
    }
}
