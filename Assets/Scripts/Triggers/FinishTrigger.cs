using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti = null;
    private GameManager _gameManager = null;

    private void Start()
    {
        _gameManager = GameManager.GetInstance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.isStoped = true;
            _confetti.Play();
            _gameManager.Finish();
        }
    }
}