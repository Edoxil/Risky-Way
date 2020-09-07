using UnityEngine;

public class TurnLeftTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out PlayerMovement _movement))
        {
            _movement.TurnLeft();
        }
    }
}