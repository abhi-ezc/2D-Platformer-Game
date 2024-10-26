using UnityEngine;

public enum PatrolDirection
{
    Left,
    Right,
}

public class EnemyController : MonoBehaviour
{
    // public
    public bool canPatrol;
    public float moveSpeed;
    public float restDuration = 3.0f;
    public Vector2 leftMostPatrolEndPoint;
    public Vector2 rightMostPatrolEndPoint;
    public Animator animator;

    // animator keys
    readonly int speedAnimatorKey = Animator.StringToHash("Speed");

    // private
    bool isTakingRest = true;

    // patrol
    PatrolDirection nextPatrolDirection = PatrolDirection.Left;
    PatrolDirection patrolDirection = PatrolDirection.Left;
    float restTimer;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;

    void Awake ()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        MoveEnemy();
    }
    void OnCollisionEnter2D (Collision2D col)
    {
        PlayerController playerController = col.gameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.KillPlayer();
        }
    }

    void MoveEnemy ()
    {
        if (canPatrol)
        {
            UpdatePatrolDirection();
            if (!isTakingRest)
            {
                UpdateLocation();
            }
            else
            {
                UpdateRestTimer();
            }
        }
    }

    void UpdateRestTimer ()
    {
        restTimer += Time.deltaTime;
        if (restTimer >= restDuration)
        {
            restTimer = 0;
            isTakingRest = false;
            patrolDirection = nextPatrolDirection;
        }
        else
        {
            animator.SetFloat(speedAnimatorKey, 0);
        }
    }

    void UpdatePatrolDirection ()
    {
        if (transform.position.x <= leftMostPatrolEndPoint.x && patrolDirection == PatrolDirection.Left)
        {
            nextPatrolDirection = PatrolDirection.Right;
            isTakingRest = true;
        }
        else if (transform.position.x >= rightMostPatrolEndPoint.x && patrolDirection == PatrolDirection.Right)
        {
            nextPatrolDirection = PatrolDirection.Left;
            isTakingRest = true;
        }
    }

    void UpdateLocation ()
    {
        Vector3 newPos = Vector3.zero;
        float speed = moveSpeed * Time.deltaTime;
        switch (patrolDirection)
        {
            case PatrolDirection.Left:
                spriteRenderer.flipX = true;
                speed *= -1;
                break;
            case PatrolDirection.Right:
                spriteRenderer.flipX = false;
                break;
        }
        newPos.x = speed;
        animator.SetFloat(speedAnimatorKey, 1);
        transform.position += newPos;
    }
}
