using DG.Tweening;
using UnityEngine;

public class Edge : MonoBehaviour
{
    private Vector3 _defaultRot;
    private float _cutAngel = 20f;
    private ForwarAxis _forwardAxis = ForwarAxis.X;
    private enum ForwarAxis
    {
        X,Z
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Slash();

        }
    }
    public void TurnLeftHandler()
    {
        _forwardAxis = ForwarAxis.Z;
    }
    public void Slash()
    {
        Vector3 rotation = Vector3.zero;

        if (_forwardAxis== ForwarAxis.X)
        {
            _defaultRot = transform.localEulerAngles;
            rotation = Vector3.right * _cutAngel;
        }
        else if(_forwardAxis== ForwarAxis.Z)
        {

            _defaultRot = transform.localEulerAngles;
            rotation = Vector3.right * _cutAngel;
        }

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOLocalRotate(rotation, 0.1f));
        seq.AppendInterval(0.05f);
        seq.Append(transform.DOLocalRotate(_defaultRot, 0.1f));
        seq.Play();
    }
}

