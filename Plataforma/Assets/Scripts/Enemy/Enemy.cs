using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Vector2 knockbackToSelf = new Vector2(6f, 10f);
    [SerializeField] private Vector2 knockbackToPlayer = new Vector2(3f, 5f);
    [SerializeField] private float knockbackDelayToSelf = 1.5f;

    [SerializeField] private int damage = 3;
    
    public void Die() {
        Destroy(gameObject);
    }

    public void HitPlayer(Transform playerTransform) {
        int direction = GetDirection(playerTransform);
        FindObjectOfType<PlayerMovement>().KnockbackPlayer(knockbackToPlayer, direction);
        FindObjectOfType<PlayerHealth>().DamagePlayer(damage);
        GetComponent<EnemyMovement>().KnockbackEnemy(knockbackToSelf, -direction, knockbackDelayToSelf);
    }

    private int GetDirection(Transform playerTransform) {
        if (transform.position.x > playerTransform.position.x) {
            return -1;
        } else {
            return 1;
        }
    }
}
