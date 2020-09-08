using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateWall : MonoBehaviour
{

    [SerializeField] private Rigidbody[] _cratesBodies = null;
    [SerializeField] private List<Crate> _crates = null;
    private PlayerResurces _playerResurses = null;


    private void Start()
    {
        _playerResurses = (PlayerResurces)FindObjectOfType(typeof(PlayerResurces));
        _crates = new List<Crate>();
        foreach (Rigidbody body in _cratesBodies)
        {
            _crates.Add(body.GetComponent<Crate>());
        }
        
    }



    public void ExplodeHandler()
    {

        foreach (Rigidbody body in _cratesBodies)
        {
            Vector3 pos = body.position;
            pos.x -= 0.5f;
            pos.y -= 0.5f;
            body.isKinematic = false;
            body.AddForceAtPosition(Vector3.up * 15f, pos, ForceMode.Impulse);
        }
        _playerResurses?.MultiCoinColected(_crates.Count);

        StartCoroutine(DestroyCrates());

        Destroy(gameObject, 3f);
    }


    private IEnumerator DestroyCrates()
    {
        yield return new WaitForSeconds(0.7f);
        foreach (Crate crate in _crates)
        {
            crate.Interact();
        }
        StopAllCoroutines();
    }
}
