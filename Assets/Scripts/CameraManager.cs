using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Singleton
    private static CameraManager _instance = null;
    public static CameraManager GetInstance()
    {
        return _instance;
    }
    void Awake()
    {

        if (_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion Singleton

    [SerializeField] private CinemachineFreeLook _camera = null;

    public void StopFolowing()
    {
        _camera.Follow = null;
        _camera.LookAt = null;
    }
    public void StartFolowing(Transform target)
    {
        _camera.Follow = target;
        _camera.LookAt = target;
    }
}
