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
        else if (other.CompareTag("VirtualHelp"))
        {
            other.GetComponent<DialogueUI>().OnDialogueRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("VirtualHelp"))
        {
            other.GetComponent<DialogueUI>().OnDialogueRange(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (GameController.Instance.CurrentGameState != GameController.GameState.Active)
            return;

        switch (other.gameObject.tag)
        {
            case "Enemy":
                CollideWithEnemy(other);
                break;

            case "Trophy":
                HandleTrophyCollision(other, GameController.GameState.Complete);
                break;

            case "TrophyWin":
                HandleTrophyCollision(other, GameController.GameState.Win);
                break;
        }
    }

    private void HandleTrophyCollision(Collision2D other, GameController.GameState targetState)
    {
        if (transform.position.y >= other.transform.position.y)
        {
            GameController.Instance.SetGameState(targetState);
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
