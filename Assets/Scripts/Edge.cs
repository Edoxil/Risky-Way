using DG.Tweening;
using UnityEngine;

public class Edge : MonoBehaviour
{

    public Vector3 qwerty;
    Vector3 defaultRot;

    private void Start()
    {
        defaultRot = transform.localEulerAngles;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Slash();

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
           
            defaultRot = new Vector3(0f, 0f, 0f);

            Vector3 rotation = Vector3.forward * 50f;

            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DORotate(rotation, 0.1f));
            seq.AppendInterval(0.1f);
            seq.Append(transform.DORotate(defaultRot, 0.1f));
            seq.Play();
        }


    }

    public void Slash()
    {


        Vector3 rotation = Vector3.right * 50f;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DORotate(rotation, 0.1f));
        seq.AppendInterval(0.1f);
        seq.Append(transform.DORotate(defaultRot, 0.1f));
        seq.Play();
    }

}



