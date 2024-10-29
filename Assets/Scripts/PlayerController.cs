using System;
using Sounds;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    // Instance Assignments
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    public Rigidbody2D rgdb2D;
    public ScoreController scoreController;
    public LifeUIController lifeUIController;
    public float speed;
    public float jumpForce = 6;
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
    bool isDead = false;

    // footsteps
    public float minTimeBetweenFootsteps = 0.3f;
    public float maxTimeBetweenFootsteps = 0.6f;
    private float timeSinceLastFootstep;

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
            CheckCanJump();
        }
    }

    void MoveCharacter ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (CanMove(horizontal))
        {
            Vector3 pos = transform.position;
            pos.x += horizontal * Time.deltaTime * speed;
            transform.position = pos;

            PlayHorizontalMovementAnimation(horizontal);
            PlayFootSound(horizontal);
        }
        else
        {
            PlayHorizontalMovementAnimation(0);
        }

    }

    private bool CanMove (float horizontal)
    {
        return !isDead;
    }

    void PlayFootSound (float horizontal)
    {
        if (Math.Abs(horizontal) > 0.25 && rgdb2D.velocity.y == 0)
        {
            if (Time.time - timeSinceLastFootstep >= Random.Range(minTimeBetweenFootsteps, maxTimeBetweenFootsteps))
            {
                SoundManager.Instance.Play(ESound.PlayerMove);
                timeSinceLastFootstep = Time.time;
            }
        }
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

    void CheckCanJump ()
    {
        float vertical = Input.GetAxis("Jump");
        if (vertical > 0 && rgdb2D.velocity.y == 0)
        {
            animator.SetBool(IsCrouchingAnimatorKey, false);
            rgdb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
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
        SoundManager.Instance.Play(ESound.PlayerDeath);
        isDead = true;
        animator.SetTrigger(DeathAnimatorKey);
        GameManager.Instance.OnGameOver();
        Destroy(this);
    }
}
