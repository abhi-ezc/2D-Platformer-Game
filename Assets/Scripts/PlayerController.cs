
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
     
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
        UpdateVelocity();
        CheckIfCrouching();
        CheckIfJumping();
    }

    private void UpdateVelocity()
    {
        float speed = Input.GetAxis("Horizontal");
        animator.SetFloat(VelocityAnimatorKey, Mathf.Abs(speed));
        if (speed < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (speed > 0)
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

    private void CheckIfCrouching()
    {
        bool isCrouching = !animator.GetBool(IsCrouchingAnimatorKey);
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (isCrouching)
            {
                boxCollider2D.offset = CrouchedHeightColliderMetrics.position;
                boxCollider2D.size = CrouchedHeightColliderMetrics.size;
            }
            else
            {
                boxCollider2D.offset = FullHeightColliderMetrics.position;
                boxCollider2D.size = FullHeightColliderMetrics.size;
            }
            animator.SetBool(IsCrouchingAnimatorKey,isCrouching);
        }
    }

    private void CheckIfJumping()
    {
        float vertical = Input.GetAxis("Vertical");
        if (vertical > 0)
        {
            animator.SetBool(IsCrouchingAnimatorKey, false);
            animator.SetTrigger(JumpAnimatorKey);
        }
        else
        {
            animator.ResetTrigger(JumpAnimatorKey);
        }
    }

}
