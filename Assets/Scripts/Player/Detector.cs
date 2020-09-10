using UnityEngine;
using UnityEngine.Events;

public class Detector : MonoBehaviour
{
    private PlayerReaction _reaction;

    public UnityEvent Detected;


    private void Start()
    {
        _reaction = GetComponentInParent<PlayerReaction>();
    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);

       
        if (Physics.Raycast(ray, out hit, 2f))
        {
            if (hit.transform.TryGetComponent(out Iinteractable interatable))
            {
                Detected.Invoke();
                interatable.Interact();
                _reaction.ReactionFor(interatable);
            }
        }



        ray = new Ray(transform.position, Vector3.forward);
        if (Physics.Raycast(ray, out hit, 1f))
        {

            if (hit.transform.CompareTag("Vertical Obstacle"))
            {
                _reaction.VerticalCollision();
            }
        }
    }
}
