using UnityEngine;

public class RayShootTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<RayShooter>(out RayShooter rayShooter))
        {
            rayShooter.TurnOn();
        }
    }
}
