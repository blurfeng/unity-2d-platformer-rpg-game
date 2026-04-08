using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera _mainCamera;
    private Vector2 _lastCameraPosition;
    private float _cameraHalfWidth;
    
    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        _mainCamera = Camera.main;
        if (_mainCamera)
        {
            _cameraHalfWidth = _mainCamera.orthographicSize * _mainCamera.aspect;
            _lastCameraPosition =  _mainCamera.transform.position;
        }
        InitializeLayers();
    }

    private void FixedUpdate()
    {
        Vector2 currentCameraPosition = _mainCamera.transform.position;
        Vector2 distanceToMove = currentCameraPosition - _lastCameraPosition;
        _lastCameraPosition = currentCameraPosition;
        
        float cameraLeftEdge = currentCameraPosition.x - _cameraHalfWidth;
        float cameraRightEdge = currentCameraPosition.x + _cameraHalfWidth;

        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
        }
    }

    private void InitializeLayers()
    {
        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.CalculateImageWidth();
        }
    }
}
