using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class PlayerReaction : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement = null;
    [SerializeField] private SwipeInput _input = null;
    private CameraManager _cameraManager = null;
    private Renderer _renderer = null;
    private Detector _detector = null;

    private Vector3 _checkPoint = Vector3.zero;

    // Эвенты для обновления ресуров игрока
    public UnityEvent CoinColected;
    public UnityEvent HeartColected;
    public UnityEvent DamageTaken;


    private void Start()
    {
        _detector = GetComponentInChildren<Detector>();
        _cameraManager = CameraManager.GetInstance();
        _renderer = GetComponent<Renderer>();
    }


    // Обработка реакции на разные объекты на сцене и вызов нужного события в зависимости от типа объекта
    public void ReactionFor(Iinteractable obj)
    {
        if (obj.GetType().Equals(typeof(Heart)))
        {
            HeartColected?.Invoke();
            return;
        }
        if (obj.GetType().Equals(typeof(Coin)))
        {
            CoinColected?.Invoke();
            return;
        }
        if (obj.GetType().Equals(typeof(Crate)))
        {
            CoinColected?.Invoke();
            return;
        }

        // Если объект не относится к безопасным типам то наносится урон
        if (obj.GetType().Equals(typeof(Fire)))
        {
            FireReaction();
            TakeDamage();
            return;
        }

        Blinking();
        TakeDamage();
    }

    // Обработка столкновения с вертикальным объектом 
    public void VerticalCollision()
    {
        TakeDamage();
        Blinking();
        int lives = GetComponentInParent<PlayerResources>().GetLives();
        if (lives >= 1)
        {
            ReturnToCheckPoint();
        }
    }



    // Возврат к последниму чекпоинту либо на старт 
    private void ReturnToCheckPoint()
    {
        _playerMovement.enabled = false;
        _detector.enabled = false;
        _input.enabled = false;
        _playerMovement.transform.DOMove(_checkPoint, 1f)
            .onComplete += () =>
            {
                _detector.enabled = true;
                _playerMovement.enabled = true;
                _input.enabled = true;
            };
        _playerMovement.lane = PlayerMovement.Lane.Mid;
    }





    // Отправляем событие о получении урона в PlayerResurces 
    private void TakeDamage()
    {
        DamageTaken?.Invoke();
    }

    // Мигание ножа
    private void Blinking()
    {
        _renderer.material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");

        Color normal = Color.white;
        Color transparent = Color.white;

        transparent.a = 0.25f;

        Sequence seq = DOTween.Sequence();

        seq.onComplete += SetDefoultShader;
        for (int i = 0; i < 3; i++)
        {
            seq.Append(_renderer.material.DOColor(transparent, 0.1f));
            seq.AppendInterval(0.1f);
            seq.Join(_renderer.material.DOColor(normal, 0.1f));
            seq.AppendInterval(0.1f);
        }
        seq.Play();

    }
    // Делаем нож красным
    private void FireReaction()
    {
        _renderer.material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        Color normal = Color.white;
        Color red = Color.red;

        Sequence seq = DOTween.Sequence();
        seq.onComplete += SetDefoultShader;
        seq.Append(_renderer.material.DOColor(red, 0.5f));
        seq.AppendInterval(0.3f);
        seq.Append(_renderer.material.DOColor(normal, 0.5f));
        seq.Play();
    }
    private void SetDefoultShader()
    {
        _renderer.material.shader = Shader.Find("Mobile/Diffuse");

    }




    // Реакция на смерть игрока. Останавливаем следование камеры и отправляем нож за пределы пути
    public void PlayerDiedHandler()
    {
        Transform player = _playerMovement.GetComponent<Transform>();
        NavMeshAgent agent = _playerMovement.GetComponent<NavMeshAgent>();
        _cameraManager.StopFolowing();
        _playerMovement.isStoped = true;
        _detector.enabled = false;
        agent.enabled = false;

        Sequence seq = DOTween.Sequence();
        seq.onComplete += () => { _detector.enabled = true; };

        seq.Append(player.DOLocalMoveY(3f, 0.5f));
        seq.AppendInterval(0.1f);
        seq.Append(player.DOLocalMoveX(5f, 0.5f));
        seq.AppendInterval(0.1f);
        seq.Append(player.DOLocalMoveY(-8f, 0.5f));
        seq.Play();
    }
    public void SetCheckPoint(Vector3 checkpoint)
    {
        checkpoint.y = 1.5f;
        _checkPoint = checkpoint;
    }
    public void GameStartHandler()
    {
        _checkPoint = new Vector3(0f, 1.5f, 0f);
    }

}