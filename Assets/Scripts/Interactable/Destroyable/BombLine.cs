using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLine : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _cratesBodiesLeft = null;
    [Space]
    [SerializeField] private Rigidbody[] _cratesBodiesRight = null;
    private List<Crate> _cratesLeft = null;
    private List<Crate> _cratesRight = null;
    private PlayerResources _playerResurses = null;


    private void Start()
    {
        _playerResurses = (PlayerResources)FindObjectOfType(typeof(PlayerResources));
        _cratesLeft = new List<Crate>();
        _cratesRight = new List<Crate>();

        foreach (Rigidbody body in _cratesBodiesLeft)
        {
            _cratesLeft.Add(body.GetComponent<Crate>());
        }


        foreach (Rigidbody body in _cratesBodiesRight)
        {
            _cratesRight.Add(body.GetComponent<Crate>());
        }
    }

    public void ExplodeMidBombHandler()
    {

        foreach (Rigidbody body in _cratesBodiesLeft)
        {
            Vector3 pos = body.position;
            pos.y = -0.5f;
            pos.x = 0.4f;
            body.isKinematic = false;
            body.AddForceAtPosition(Vector3.up * 15f, pos, ForceMode.Impulse);
        }

        foreach (Rigidbody body in _cratesBodiesRight)
        {
            Vector3 pos = body.position;
            pos.y = -0.5f;
            pos.x = -0.4f;
            body.isKinematic = false;
            body.AddForceAtPosition(Vector3.up * 15f, pos, ForceMode.Impulse);
        }
        _playerResurses?.MultiCoinColected(_cratesRight.Count + _cratesLeft.Count);


        StartCoroutine(DestroyAllCrates());

        Destroy(gameObject, 3f);
    }


    public void ExplodeSideBombHandler()
    {

        foreach (Rigidbody body in _cratesBodiesLeft)
        {
            Vector3 pos = body.position;
            pos.y = -0.5f;
            pos.x = -0.4f;
            body.isKinematic = false;
            body.AddForceAtPosition(Vector3.up * 12f, pos, ForceMode.Impulse);
        }

        _playerResurses?.MultiCoinColected(_cratesLeft.Count);
        StartCoroutine(DestroyLeftCrates());
        Destroy(gameObject, 3f);
    }

    private IEnumerator DestroyLeftCrates()
    {
        yield return new WaitForSeconds(0.3f);
        foreach (Crate crate in _cratesLeft)
        {
            crate.Interact();
        }
        StopAllCoroutines();
    }

    private IEnumerator DestroyAllCrates()
    {
        yield return new WaitForSeconds(0.3f);
        foreach (Crate crate in _cratesLeft)
        {
            crate.Interact();
        }
        foreach (Crate crate in _cratesRight)
        {
            crate.Interact();
        }
        StopAllCoroutines();
    }
}
