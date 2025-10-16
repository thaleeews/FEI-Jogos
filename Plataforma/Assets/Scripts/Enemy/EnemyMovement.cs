using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed = 3f;
    [SerializeField] private int startDirection = 1;
    private int currentDirection;
    private float halfWidth;
    private float halfHeight;
    private Vector2 movement;
    [SerializeField] private bool paraNaBeirada = true;
    private bool isGrounded;
    private float movementDelay;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        halfWidth = spriteRenderer.bounds.extents.x;
        halfHeight = spriteRenderer.bounds.extents.y;
        currentDirection = startDirection;
        spriteRenderer.flipX = startDirection == 1 ? false : true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (movementDelay > 0f) {
            movementDelay -= Time.fixedDeltaTime;
            return;
        }
        movement.x = currentDirection * speed;
        movement.y = rigidBody.linearVelocity.y;
        rigidBody.linearVelocity = movement;
        SetDirection();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {        
        isGrounded = false;
    }    

    public void KnockbackEnemy(Vector2 knockbackForce, int direction, float delay)
    {
        movementDelay = delay;
        knockbackForce.x *= direction;
        rigidBody.linearVelocity = Vector2.zero;
        rigidBody.angularVelocity = 0f;
        rigidBody.AddForce(knockbackForce, ForceMode2D.Impulse);
    }

    private void SetDirection()
    {
        if (!isGrounded) return;
        Vector2 rightPos = transform.position;
        Vector2 leftPos = transform.position;
        rightPos.x += halfWidth;
        leftPos.x -= halfWidth;

        if(rigidBody.linearVelocity.x > 0) {
            if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth - 0.1f, LayerMask.GetMask("Ground")) && 
            rigidBody.linearVelocity.x > 0)
            {
                currentDirection *= -1;
                spriteRenderer.flipX = true;
            }
            else if (paraNaBeirada && !Physics2D.Raycast(rightPos, Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Ground")))
            {
                currentDirection *= -1;
                spriteRenderer.flipX = true;
            }
        } else if(rigidBody.linearVelocity.x < 0) {
            if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth - 0.1f, LayerMask.GetMask("Ground")) && 
            rigidBody.linearVelocity.x < 0)
            {
                currentDirection *= -1;
                spriteRenderer.flipX = false;
            }
            else if (paraNaBeirada && !Physics2D.Raycast(leftPos, Vector2.down, halfHeight + 0.1f, LayerMask.GetMask("Ground")))
            {
                currentDirection *= -1;
                spriteRenderer.flipX = false;
            }
        }
    }
}
