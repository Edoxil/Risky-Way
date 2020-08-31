using UnityEngine;

public class RayShootTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {

        RayShooter rayShooter = other.GetComponentInChildren<RayShooter>();

        if (rayShooter != null)
        {

            rayShooter.TurnOn();
            
        }

    }
}
