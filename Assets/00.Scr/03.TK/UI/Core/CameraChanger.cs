using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    private Camera _mainCam;
    private Camera _uiCam;

    private bool _isUIMode = false;

    private void Awake()
    {
        _mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        _uiCam = GameObject.Find("UICamera").GetComponent<Camera>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            CameraModeChange(_isUIMode);
        }
    }

    private void CameraModeChange(bool isActive)
    {
        if (_isUIMode)
        {
            _mainCam.depth = 2;
            _uiCam.depth = 1;
        }
        else if (!_isUIMode)
        {
            _mainCam.depth = 1;
            _uiCam.depth = 2;
        }
        _isUIMode = !_isUIMode;
    }
}
