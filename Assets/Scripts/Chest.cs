using UnityEngine;

public class Chest : MonoBehaviour, IDamageable
{
    private static readonly int _open = Animator.StringToHash("open");
    private Animator _animator;
    private Rigidbody2D _rb;
    private EntityVFX _vfx;
    
    [Header("Open")]
    [SerializeField] protected Vector2 knockback = new Vector2(0.8f, 5f);

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _vfx = GetComponent<EntityVFX>();
        _animator = GetComponentInChildren<Animator>();
    }

    public void TakeDamage(float damage, Transform damageDealer)
    {
        _animator.SetBool(_open, true);
        _vfx?.PlayOnDamageVfs();
        Vector3 dir = transform.position - damageDealer.position;
        _rb.linearVelocity = new Vector2(knockback.x * Mathf.Sign(dir.x), knockback.y);
        _rb.angularVelocity = Random.Range(-200f, 200f);
        
        // 掉落道具。
    }
}