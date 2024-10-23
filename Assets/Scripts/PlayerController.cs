
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    public float speed;
    public float jumpForce;
    public Rigidbody2D rigidbody2D;
     
    // Animator Parameter Keys
    private static readonly int VelocityAnimatorKey = Animator.StringToHash("Velocity");
    private static readonly int IsCrouchingAnimatorKey = Animator.StringToHash("IsCrouching");
    private static readonly int JumpAnimatorKey = Animator.StringToHash("Jump");
    
    // Box Collider Sizes and Offset
    private readonly Rect FullHeightColliderMetrics = new Rect(new Vector2(0f, 1f), new Vector2(0.5f, 2f));
    private readonly Rect CrouchedHeightColliderMetrics = new Rect(new Vector2(-0.13f, 0.6f),new Vector2(0.9f,1.31f));
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        CheckCanCrouch();
        CheckIfJumping();
    }

    private void MoveCharacter()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 pos = transform.position;
        pos.x += horizontal * Time.deltaTime * speed;
        transform.position = pos;
        
        PlayHorizontalMovementAnimation(horizontal);
    }

    private void PlayHorizontalMovementAnimation(float horizontal)
    {
        animator.SetFloat(VelocityAnimatorKey, Mathf.Abs(horizontal));
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            // do nothing
            // it will preserve the last flipx
            // if no movement the player sprite will look at the last direction
        }
    }

    private void CheckCanCrouch()
    {
        bool canCrouch = !animator.GetBool(IsCrouchingAnimatorKey);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (canCrouch)
            {
                boxCollider2D.offset = CrouchedHeightColliderMetrics.position;
                boxCollider2D.size = CrouchedHeightColliderMetrics.size;
            }
            else
            {
                boxCollider2D.offset = FullHeightColliderMetrics.position;
                boxCollider2D.size = FullHeightColliderMetrics.size;
            }
            animator.SetBool(IsCrouchingAnimatorKey, canCrouch);
        }
    }

    private void CheckIfJumping()
    {
        float vertical = Input.GetAxis("Vertical");
        if (vertical > 0)
        {
            animator.SetBool(IsCrouchingAnimatorKey, false);
            Vector2 force = new Vector2(0, jumpForce );
            rigidbody2D.AddForce(force);
            animator.SetTrigger(JumpAnimatorKey);
        }
        else
        {
            animator.ResetTrigger(JumpAnimatorKey);
        }
    }

}
