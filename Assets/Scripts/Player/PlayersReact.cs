using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PlayersReact : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement = null;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage();

        }
    }
    public void ReturnToCheckPoint()
    {
        TakeDamage();
        _playerMovement.isStoped = true;
        _playerMovement.transform.DOMove(new Vector3(0.1f, 1.5f, 0f), 1f);
       
    }
    public void TakeDamage()
    {

        Blinking();
    }

        

    private void Blinking()
    {
        _renderer.material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");

        Color normal = Color.white;
        Color transparent = Color.white;

        transparent.a = 0.25f;

        Sequence seq = DOTween.Sequence();
        seq.Append(_renderer.material.DOColor(transparent, 0.1f));
        seq.AppendInterval(0.1f);
        seq.Join(_renderer.material.DOColor(normal, 0.1f));
        seq.AppendInterval(0.1f);
        seq.Join(_renderer.material.DOColor(transparent, 0.1f));
        seq.AppendInterval(0.1f);
        seq.Join(_renderer.material.DOColor(normal, 0.1f));
        seq.AppendInterval(0.1f);
        seq.Join(_renderer.material.DOColor(transparent, 0.1f));
        seq.AppendInterval(0.1f);
        seq.Join(_renderer.material.DOColor(normal, 0.1f));
        seq.AppendInterval(0.1f);
        seq.Join(_renderer.material.DOColor(transparent, 0.1f));
        seq.AppendInterval(0.1f);
        seq.Join(_renderer.material.DOColor(normal, 0.1f));
        seq.AppendInterval(0.1f);
        seq.Join(_renderer.material.DOColor(transparent, 0.1f));
        seq.AppendInterval(0.1f);
        seq.Join(_renderer.material.DOColor(normal, 0.1f));
        seq.Play();

        StartCoroutine(WaitForBlinkingFinished(seq));
    }
    private IEnumerator WaitForBlinkingFinished(Sequence sequence)
    {
        yield return sequence.WaitForCompletion();
        _renderer.material.shader = Shader.Find("Mobile/Diffuse");
        StopAllCoroutines();
    }

}
