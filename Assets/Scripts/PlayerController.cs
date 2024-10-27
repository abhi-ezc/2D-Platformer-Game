using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Instance Assignments
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    public Rigidbody2D rigidbody2D;
    public ScoreController scoreController;
    public LifeUIController lifeUIController;
    public float speed;
    public float jumpForce;
    public int playerLives = 3;

    // Box Collider Sizes and Offset
    readonly Rect _crouchedHeightColliderMetrics = new Rect(new Vector2(-0.13f, 0.6f), new Vector2(0.9f, 1.31f));
    readonly Rect _fullHeightColliderMetrics = new Rect(new Vector2(0f, 1f), new Vector2(0.5f, 2f));
    readonly int DeathAnimatorKey = Animator.StringToHash("Death");
    readonly int IsCrouchingAnimatorKey = Animator.StringToHash("IsCrouching");
    readonly int JumpAnimatorKey = Animator.StringToHash("Jump");

    // Animator Parameter Keys
    readonly int VelocityAnimatorKey = Animator.StringToHash("Velocity");

    // Others
    bool isDead;

    void Awake ()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start ()
    {
        lifeUIController.SetCount(playerLives);
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isDead)
        {
            MoveCharacter();
            CheckCanCrouch();
            CheckIfJumping();
        }
    }

    void MoveCharacter ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 pos = transform.position;
        pos.x += horizontal * Time.deltaTime * speed;
        transform.position = pos;

        PlayHorizontalMovementAnimation(horizontal);
    }

    void PlayHorizontalMovementAnimation (float horizontal)
    {
        animator.SetFloat(VelocityAnimatorKey, Mathf.Abs(horizontal));
        if (horizontal < 0)
            spriteRenderer.flipX = true;
        else if (horizontal > 0) spriteRenderer.flipX = false;
    }

    void CheckCanCrouch ()
    {
        bool canCrouch = !animator.GetBool(IsCrouchingAnimatorKey);

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (canCrouch)
            {
                boxCollider2D.offset = _crouchedHeightColliderMetrics.position;
                boxCollider2D.size = _crouchedHeightColliderMetrics.size;
            }
            else
            {
                boxCollider2D.offset = _fullHeightColliderMetrics.position;
                boxCollider2D.size = _fullHeightColliderMetrics.size;
            }

            animator.SetBool(IsCrouchingAnimatorKey, canCrouch);
        }
    }

    void CheckIfJumping ()
    {
        float vertical = Input.GetAxis("Vertical");
        if (vertical > 0)
        {
            animator.SetBool(IsCrouchingAnimatorKey, false);
            Vector2 force = new Vector2(0, jumpForce);
            rigidbody2D.AddForce(force);
            animator.SetTrigger(JumpAnimatorKey);
        }
        else
        {
            animator.ResetTrigger(JumpAnimatorKey);
        }
    }

    public void PickupKey ()
    {
        Debug.Log("Player picked the key");
        scoreController.IncreaseScore(10);
    }

    public void OnHit ()
    {
        playerLives -= 1;
        lifeUIController.SetCount(playerLives);
        if (playerLives <= 0)
        {
            KillPlayer();
        }
    }
    public void KillPlayer ()
    {
        isDead = true;
        animator.SetTrigger(DeathAnimatorKey);
        GameManager.Instance.OnGameOver();
    }
}
