using DG.Tweening;
using UnityEngine;

public class DestroyableCrate : MonoBehaviour
{

    [SerializeField] private float _delay = 1f;

    private void Start()
    {
        Destroy(gameObject, _delay);
        
    }
       
}
