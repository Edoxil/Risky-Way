using UnityEngine;
using UnityEngine.Events;

public class Detector : MonoBehaviour
{
    public UnityEvent Detected;

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

        if(Physics.Raycast(ray,out hit, 2f))
        {
            if(hit.transform.TryGetComponent(out Iinteractable interatable))
            {
                Detected.Invoke();
                interatable.Interact();
            }
        }
    }
}
