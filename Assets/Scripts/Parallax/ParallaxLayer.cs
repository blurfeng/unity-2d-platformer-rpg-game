using System;
using UnityEngine;

[Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField, Range(0f, 1f), Tooltip("视差系数，为1时不移动（跟随相机），越接近0移动越快。")]
    private float parallaxMultiplier;

    [SerializeField] private float imageWidthOffset = 10f;
    
    private float _imageWidth;
    private float _imageHalfWidth;

    public void CalculateImageWidth()
    {
        _imageWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        _imageHalfWidth = _imageWidth / 2f;
    }
    
    public void Move(float distanceToMove)
    {
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier);
    }

    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge)
    {
        float imageRightEdge = background.position.x + _imageHalfWidth - imageWidthOffset;
        float imageLeftEdge = background.position.x - _imageHalfWidth + imageWidthOffset;

        if (imageRightEdge < cameraLeftEdge)
        {
            background.position += Vector3.right * _imageWidth;
        }
        else if (imageLeftEdge > cameraRightEdge)
        {
            background.position -= Vector3.right * _imageWidth;
        }
    }
}