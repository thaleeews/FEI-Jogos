using UnityEngine;

public class AiScript : MonoBehaviour
{
    private Rigidbody2D rb2d; 
    
    public float boundY;
    public float boundX;
    public float maxMovementSpeed;
    private Vector2 startingPosition;

    public Rigidbody2D puck_0;

    private Vector2 targetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startingPosition = rb2d.position;
    }

    private void FixedUpdate() {
        float movementSpeed;

        if (puck_0.position.y < 0) {
            movementSpeed = maxMovementSpeed * Random.Range(0.1f, 0.3f);
            targetPosition = new Vector2(Mathf.Clamp(puck_0.position.x, -boundX, boundX), startingPosition.y);
        }
        else {
            movementSpeed = Random.Range(maxMovementSpeed * 0.4f, maxMovementSpeed);
            targetPosition = new Vector2(Mathf.Clamp(puck_0.position.x, -boundX, boundX), Mathf.Clamp(puck_0.position.y, 0, boundY));
        }

        rb2d.MovePosition(Vector2.MoveTowards(rb2d.position, targetPosition, movementSpeed * Time.fixedDeltaTime));
    }
}
