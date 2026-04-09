using System;
using System.Collections;
using UnityEngine;

public class EntityVFX : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    [Header("On Damage")] 
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageVfxDuration = 0.15f;
    
    private Material _originalMaterial;
    private Coroutine _onDamageVfxCoroutine;
    
    protected virtual void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalMaterial = _spriteRenderer.material;
    }

    /// <summary>
    /// 受伤时播放特效。
    /// </summary>
    public void PlayOnDamageVfs()
    {
        if (_onDamageVfxCoroutine != null)
            StopCoroutine(_onDamageVfxCoroutine);
        
        _onDamageVfxCoroutine = StartCoroutine(OnDamageVfxCo());
    }

    private IEnumerator OnDamageVfxCo()
    {
        _spriteRenderer.material = onDamageMaterial;
        
        yield return new WaitForSeconds(onDamageVfxDuration);
        _spriteRenderer.material = _originalMaterial;
    }
}
