using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float bounceForce = 6f;
    [SerializeField] private Rigidbody2D rigidBody;
    private float halfHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        halfHeight = spriteRenderer.bounds.extents.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            other.GetComponent<Fruit>().Collect();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            CollideWithEnemy(other);
        }
    }

    private void CollideWithEnemy(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (Physics2D.Raycast(transform.position, Vector2.down, halfHeight + 1f, LayerMask.GetMask("Enemy")))
        {
            Vector2 velocity = rigidBody.linearVelocity;
            velocity.y = 0f;
            rigidBody.linearVelocity = velocity;
            rigidBody.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            enemy.Die();
        }
        else
        {
            enemy.HitPlayer(transform);
        }
    }
}
