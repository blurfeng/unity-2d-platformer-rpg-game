using UnityEngine;

public class VFXController : MonoBehaviour
{
    [Header("Destroy")]
    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1f;
    
    [Header("Random Position")]
    [SerializeField] private bool randomOffset = true;
    [SerializeField] private Vector2 offsetX = new Vector2(0.3f, -0.3f);
    [SerializeField] private Vector2 offsetY = new Vector2(0.3f, -0.3f);
    [SerializeField] private bool randomRotation = true;
    [SerializeField] private Vector2 rotationRange = new Vector2(0f, 360f);

    private void Start()
    {
        ApplyRandomOffset();
        ApplyRandomRotation();
        
        if (autoDestroy)
            Destroy(gameObject, destroyDelay);
    }

    private void ApplyRandomOffset()
    {
        if (!randomOffset) 
            return;
        
        float randomX = Random.Range(offsetX.x, offsetX.y);
        float randomY = Random.Range(offsetY.x, offsetY.y);
        transform.position += new Vector3(randomX, randomY);
    }

    private void ApplyRandomRotation()
    {
        if (!randomRotation)
            return;
        
        float randomRot = Random.Range(rotationRange.x, rotationRange.y);
        transform.rotation = Quaternion.Euler(0f, 0f, randomRot);
    }
}
