using System;
using UnityEngine;

[Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField, Range(0f, 1f), Tooltip("视差系数，为1时不移动（跟随相机），越接近0移动越快。")]
    private float parallaxMultiplier;

    [SerializeField, Tooltip("背景图像宽度的偏移量，用于调整循环背景时的边界检测，避免出现缝隙。")]
    private float imageWidthOffset = 10f;
    
    private float _imageWidth;
    private float _imageHalfWidth;

    public void CalculateImageWidth()
    {
        _imageWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        _imageHalfWidth = _imageWidth / 2f;
    }
    
    public void Move(Vector2 distanceToMove)
    {
        background.position += (Vector3)distanceToMove * parallaxMultiplier;
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