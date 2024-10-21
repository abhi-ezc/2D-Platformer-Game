
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRenderer;
     

    private static readonly int Velocity = Animator.StringToHash("Velocity");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetAxis("Horizontal");
        animator.SetFloat(Velocity,Mathf.Abs(speed));
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
            Debug.Log(speed);
            // do nothing
            // it will preserve the last flipx
            // if no movement the player sprite will look at the last direction
        }
    }

}
