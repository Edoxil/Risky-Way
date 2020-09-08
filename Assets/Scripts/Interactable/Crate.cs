using UnityEngine;

public class Crate : MonoBehaviour, Iinteractable
{
    [SerializeField] private GameObject _destoyedPrefab = null;
    [SerializeField] private Coin _coin = null;

    private float _randX;
    private float _randY;



    private void Update()
    {
        _randX = Random.Range(-15f, 15f);
        _randY = Random.Range(-10f, 10f);
    }

    public void Interact()
    {

        Vector3 randRot = new Vector3(_randX, _randY, 0f);
        Vector3 pos = transform.position;
        transform.DetachChildren();
        _coin.Interact();
        Instantiate(_destoyedPrefab, pos, Quaternion.Euler(randRot));

        Destroy(gameObject);
    }

}