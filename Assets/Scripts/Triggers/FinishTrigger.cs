using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti = null;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().isStoped = true;
            _confetti.Play();

        }
    }
}