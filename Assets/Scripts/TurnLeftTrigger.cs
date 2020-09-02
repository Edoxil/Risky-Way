using UnityEngine;
using UnityEngine.Events;

public class TurnLeftTrigger : MonoBehaviour
{
    public UnityEvent TurnLeft;
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("Player"))
        {
            TurnLeft?.Invoke();
            
        }

    }
}
