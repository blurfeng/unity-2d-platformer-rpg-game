using System.Collections;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public StateMachine StateMachine { get; private set; }

    public bool facingRight = true;
    public int FacingDir { get; private set; } = 1;

    [Header("Collision detection")]
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 1.1f;
    [SerializeField] private float wallCheckDistance = 0.4f;
    [SerializeField] private Transform forwardGroundCheck;
    [SerializeField] private float forwardGroundCheckDistance = 1.1f;
    [SerializeField] private Transform primaryWallCheck;
    [SerializeField] private Transform secondaryWallCheck;
    
    /// <summary>
    /// 是否接触地面，基于groundCheckDistance和groundLayer进行检测。
    /// </summary>
    public bool IsGrounded {get; private set;}
    
    /// <summary>
    /// 是否前方接触地面，基于groundCheckDistance和groundLayer进行检测。
    /// </summary>
    public bool IsGroundedForward {get; private set;}
    
    /// <summary>
    /// 是否接触墙壁，基于wallCheckDistance和groundLayer进行检测。
    /// </summary>
    public bool IsWallDetected {get; private set;}

    private bool _isKnocked;
    private Coroutine _knockbackCoroutine;

    protected virtual void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        
        StateMachine = new StateMachine();

    }
    
    protected virtual void Start()
    {
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void Update()
    {
        HandleCollisionDetected();
        StateMachine.Update(Time.deltaTime);
    }

    public void CurrentStateAnimationTrigger()
    {
        StateMachine.CurrentState.AnimationTrigger();
    }
    
    public void ReceiveKnockback(Vector2 force, float duration)
    {
        if (_knockbackCoroutine != null)
            StopCoroutine(_knockbackCoroutine);
        
        _knockbackCoroutine = StartCoroutine(Knockback(force, duration));
    }

    private IEnumerator Knockback(Vector2 force, float duration)
    {
        _isKnocked = true;
        Rb.linearVelocity = force;
        
        yield return new WaitForSeconds(duration);
        
        ClearVelocity();
        _isKnocked = false;
    }
    
    public void SetVelocity(float velocityX, float velocityY)
    {
        if (_isKnocked)
            return;
        
        Rb.linearVelocity = new  Vector2(velocityX, velocityY);
        HandleFlip(velocityX);
    }
    
    public void SetVelocityX(float velocityX)
    {
        SetVelocity(velocityX, Rb.linearVelocity.y);
    }

    public void SetVelocityY(float velocityY)
    {
        SetVelocity(Rb.linearVelocity.x, velocityY);
    }
    
    public void ClearVelocity()
    {
        Rb.linearVelocity = Vector2.zero;
    }

    public void ClearVelocityX()
    {
        Rb.linearVelocity = new  Vector2(0f, Rb.linearVelocity.y);
    }
    
    public void ClearVelocityY()
    {
        Rb.linearVelocity = new  Vector2(Rb.linearVelocity.x, 0f);
    }

    public void HandleFlip(float velocityX)
    {
        if (facingRight && velocityX < 0)
        {
            Flip();
        }
        else if (!facingRight && velocityX > 0)
        {
            Flip();
        }
    }

    public void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
        FacingDir = facingRight ? 1 : -1;
    }

    private void HandleCollisionDetected()
    {
        IsGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        if (forwardGroundCheck)
        {
            IsGroundedForward = Physics2D.Raycast(forwardGroundCheck.position, Vector2.down, forwardGroundCheckDistance, groundLayer);
        }

        if (primaryWallCheck || secondaryWallCheck)
        {
            bool primaryHit = primaryWallCheck 
                              && Physics2D.Raycast(
                                  primaryWallCheck.position, 
                                  new Vector2(FacingDir, 0f), wallCheckDistance, groundLayer);
            
            bool secondaryHit = !secondaryWallCheck 
                                || Physics2D.Raycast(
                                    secondaryWallCheck.position, 
                                    new Vector2(FacingDir, 0f), wallCheckDistance, groundLayer);
            
            IsWallDetected = primaryHit && secondaryHit;
        }

    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0f, -groundCheckDistance, 0f));
        if (forwardGroundCheck) Gizmos.DrawLine(forwardGroundCheck.position, forwardGroundCheck.position + new Vector3(0f, -forwardGroundCheckDistance, 0f));
        if (primaryWallCheck) Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new  Vector3(FacingDir * wallCheckDistance, 0f, 0f));
        if (secondaryWallCheck) Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new  Vector3(FacingDir * wallCheckDistance, 0f, 0f));
    }
}
