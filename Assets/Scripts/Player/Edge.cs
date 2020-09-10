using DG.Tweening;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private Vector3 _defaultRot = Vector3.zero;
    private float _cutAngel = 20f;



    public void Slash()
    {
        Vector3 rotation = Vector3.zero;
        rotation = Vector3.right * _cutAngel;


        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalRotate(rotation, 0.1f));
        seq.AppendInterval(0.05f);
        seq.Append(transform.DOLocalRotate(_defaultRot, 0.1f));
        seq.Play();
    }
}      