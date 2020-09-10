using UnityEngine;

public class Crate : MonoBehaviour, Iinteractable
{
    [SerializeField] private GameObject _destoyedPrefab = null;
    [SerializeField] private Coin _coin = null;
    private BoxCollider _boxColider;
    private float _randX = 0f;
    private float _randY = 0f;



    private void Start()
    {
        _boxColider = GetComponent<BoxCollider>();
        _coin.isRotated = false;
    }

    public void Interact()
    {
        _randX = Random.Range(-15f, 15f);
        _randY = Random.Range(-10f, 10f);
        _boxColider.enabled = false;
        Vector3 randRot = new Vector3(_randX, _randY, 0f);
        Vector3 pos = transform.position;
        transform.DetachChildren();
        _coin.Interact();
        Instantiate(_destoyedPrefab, pos, Quaternion.Euler(randRot));

        Destroy(gameObject);
    }

}