using System;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera _mainCamera;
    private float _lastCameraPositionX;
    private float _cameraHalfWidth;
    
    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _cameraHalfWidth = _mainCamera.orthographicSize * _mainCamera.aspect;
        CalculateBackgroundWidth();
    }

    private void FixedUpdate()
    {
        float currentCameraPositionX = _mainCamera.transform.position.x;
        float distanceToMove = currentCameraPositionX - _lastCameraPositionX;
        _lastCameraPositionX = currentCameraPositionX;
        
        float cameraLeftEdge = currentCameraPositionX - _cameraHalfWidth;
        float cameraRightEdge = currentCameraPositionX + _cameraHalfWidth;

        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
        }
    }

    private void CalculateBackgroundWidth()
    {
        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.CalculateImageWidth();
        }
    }
}
